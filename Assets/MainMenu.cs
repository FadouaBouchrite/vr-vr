using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
   public void PlaySceneArabic1()
    {
        SceneManager.LoadScene(1);
    }

    public void PlaySceneFrench1()
    {
        SceneManager.LoadScene("SexChoiceFr");
    }

    public void PlaySceneEnglish1()
    {
        SceneManager.LoadScene("SexChoiceEn");
    }

    public void PlayScenceArabic1()
    {
        SceneManager.LoadScene("SexChoiceAr");
    }

    public void ReturnToFirstScene()
    {
        SceneManager.LoadScene("LanguageChoice");
    }
}
