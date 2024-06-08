using UnityEngine;
using UnityEngine.SceneManagement;

public class passer : MonoBehaviour
{
    // Méthode appelée lors du clic sur le bouton
    public void ChargerSceneForet()
    {
        SceneManager.LoadScene("foret");
    }
}
