using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class ImageUploader : MonoBehaviour
{
    public InputField outputArea;
    public Button captureButton;

    private Texture2D capturedTexture;

    void Start()
    {
        // Associer les composants UI dans l'éditeur Unity
       
        // Ajouter un écouteur pour le bouton de capture
        captureButton.onClick.AddListener(TakePictureAndPostData);
    }

    void TakePictureAndPostData()
    {
        // Démarrer la coroutine pour capturer l'image et envoyer les données
        StartCoroutine(CaptureAndPostData());
    }

    IEnumerator CaptureAndPostData()
    {
        // Attendre la fin de la trame pour capturer l'image
        yield return new WaitForEndOfFrame();

        // Capturer l'image de l'écran
        capturedTexture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        capturedTexture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        capturedTexture.Apply();

        // Convertir l'image en tableau d'octets
        byte[] imageData = capturedTexture.EncodeToJPG();

        // Envoyer les données
        yield return PostData(imageData);
    }

    IEnumerator PostData(byte[] imageData)
    {
        outputArea.text = "Loading...";
        string uri = "http://127.0.0.1:5000/upload"; // Assurez-vous que l'URL correspond à votre backend
        WWWForm form = new WWWForm();
        form.AddBinaryData("image", imageData, "screenshot.jpg", "image/jpeg");

        using (UnityWebRequest request = UnityWebRequest.Post(uri, form))
        {
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
                outputArea.text = request.error;
            else
                outputArea.text = request.downloadHandler.text;
        }
    }
}
