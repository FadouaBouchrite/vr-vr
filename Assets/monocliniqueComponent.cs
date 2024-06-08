using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class monocliniqueComponent : MonoBehaviour
{
    // Start is called before the first frame update
    IEnumerator Start()
    {
        // Attendre 15 secondes
        yield return new WaitForSeconds(15f);

        // Charger la scène "cube"
        SceneManager.LoadScene("monoclinique");
    }
}
