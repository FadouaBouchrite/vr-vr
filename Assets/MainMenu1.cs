using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu1 : MonoBehaviour
{
    public void ExitButton()
    {
        Application.Quit();
        Debug.Log("Game closed");
    }

    public void StartGame()
    {
        SceneManager.LoadScene("SexChoiceEn1");
    }

    public void CommencerJeu()
    {
        SceneManager.LoadScene("SexChoiceFr1");
    }

    public void Back()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
