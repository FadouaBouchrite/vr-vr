using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CubiqueController : MonoBehaviour
{
    IEnumerator Start()
    {
        // Attendre 15 secondes
        yield return new WaitForSeconds(15f);

        // Charger la scène "cube"
        SceneManager.LoadScene("cube1");
    }
}