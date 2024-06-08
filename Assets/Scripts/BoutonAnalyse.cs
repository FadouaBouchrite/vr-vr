using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Ajout de la directive pour utiliser le type Text

public class BoutonAnalyse : MonoBehaviour
{
    public GameObject vitrineDeScan; // Référence à la vitrine de scan
    public Text texteResultat; // Référence à l'élément Text de l'interface utilisateur pour afficher les résultats

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        
    }

    public void AnalyserVitrine()
    {
        int nombreElements = vitrineDeScan.transform.childCount; // Nombre d'éléments dans la vitrine

        // Mettre à jour l'interface utilisateur avec les résultats
        texteResultat.text = "Nombre d'éléments dans la vitrine : " + nombreElements;
    }
}
