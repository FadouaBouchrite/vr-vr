using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Text;
 // Ajout de l'espace de noms pour la synthèse vocale
using System.IO;

public class TestConnection : MonoBehaviour
{
    public string serverURL = "http://localhost:5000/speech";
    public string openAIApiKey = "sk-proj-JTPoKUWhvlAC9mWJVO9vT3BlbkFJIw7qKBdKaN6YunJTtzFU"; // Remplacer par votre clé API OpenAI
    public AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        StartCoroutine(PostAndProcessSpeech("Votre texte de parole ici"));
    }

    IEnumerator PostAndProcessSpeech(string speechText)
    {
        // Étape 1 : Envoyer la requête POST à l'API Flask et obtenir la réponse texte
        string processedText = string.Empty;
        using (UnityWebRequest request = new UnityWebRequest(serverURL, "POST"))
        {
            SpeechData speechData = new SpeechData(speechText);
            string jsonData = JsonUtility.ToJson(speechData);
            byte[] postData = Encoding.UTF8.GetBytes(jsonData);
            request.uploadHandler = new UploadHandlerRaw(postData);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(request.error);
                yield break;
            }
            else
            {
                var jsonResponse = JsonUtility.FromJson<SpeechResponse>(request.downloadHandler.text);
                processedText = jsonResponse.text;
                Debug.Log("Processed Text: " + processedText);
            }
        }

        // Étape 2 : Utiliser OpenAI TTS pour convertir le texte en audio
        byte[] audioData = null;
        using (UnityWebRequest ttsRequest = new UnityWebRequest("https://api.openai.com/v1/audio/speech", "POST"))
        {
            var ttsPayload = new
            {
                model = "tts-1",
                voice = "alloy",
                input = processedText
            };
            string ttsJsonData = JsonUtility.ToJson(ttsPayload);
            byte[] ttsPostData = Encoding.UTF8.GetBytes(ttsJsonData);
            ttsRequest.uploadHandler = new UploadHandlerRaw(ttsPostData);
            ttsRequest.downloadHandler = new DownloadHandlerBuffer();
            ttsRequest.SetRequestHeader("Content-Type", "application/json");
            ttsRequest.SetRequestHeader("Authorization", $"Bearer {openAIApiKey}");

            yield return ttsRequest.SendWebRequest();

            if (ttsRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(ttsRequest.error);
                yield break;
            }
            else
            {
                audioData = ttsRequest.downloadHandler.data;
                Debug.Log("Audio Data Received: " + audioData.Length + " bytes");
            }
        }

        // Étape 3 : Lire et jouer l'audio reçu
        if (audioData != null && audioData.Length > 0)
        {
            // Assurez-vous que le format des données audio correspond au type de fichier attendu
            AudioClip audioClip = null;
            string tempFilePath = Path.Combine(Application.persistentDataPath, "tempAudio.mp3");

            File.WriteAllBytes(tempFilePath, audioData);
            using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip("file://" + tempFilePath, AudioType.MPEG))
            {
                yield return www.SendWebRequest();

                if (www.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogError(www.error);
                }
                else
                {
                    audioClip = DownloadHandlerAudioClip.GetContent(www);
                }
            }

            if (audioClip != null)
            {
                audioSource.clip = audioClip;
                audioSource.Play();
                Debug.Log("Playing audio");
            }
            else
            {
                Debug.LogError("Audio clip is null");
            }
        }
        else
        {
            Debug.LogError("Received audio data length is zero.");
        }
    }
}

[System.Serializable]
public class SpeechData
{
    public string speech_text;

    public SpeechData(string text)
    {
        speech_text = text;
    }
}

[System.Serializable]
public class SpeechResponse
{
    public string status;
    public string message;
    public string text;
}
