using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    // Méthode appelée lorsqu'on clique sur le bouton de démarrage
   
    
  public void StartBtn(){

 SceneManager.LoadScene("SampleScene");


  }

   public void voirMailles(){

 SceneManager.LoadScene("mailles");


  }
    // Méthode appelée lorsqu'on clique sur le bouton de quitter
    public void Quit()
    {
        // Quitter l'application
        Application.Quit();
    }


    public void voirCube(){

       
            SceneManager.LoadScene("cubiques");
        
       
 

    }
    public void voirCubeQuadratique(){

       
            SceneManager.LoadScene("cubiquesQuadratique");
        
       
 

    }  
 public void voirCubeOrthorhombique(){

       
            SceneManager.LoadScene("cubiquesOrthorhombique");
        
       
 

    }  
 public void voirCubeMonoclinique(){

       
            SceneManager.LoadScene("cubiquesMonoclinique");
        
       
 

    }  


     public void voirCubeTriclinique(){

       
            SceneManager.LoadScene("cubiquesTriclinique");
        
       
 

    }
 public void voirCubeRhomoédrique(){

       
            SceneManager.LoadScene("cubiquesrhomoédrique");
        
       
 

    }
    
    public void voirCubeHexagonale(){

       
            SceneManager.LoadScene("cubiqueshexagonale");
        
       
 

    }
}

