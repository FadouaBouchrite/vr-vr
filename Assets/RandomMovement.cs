using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class RandomMovement : MonoBehaviour
{
    public float speed = 5f; // Vitesse de déplacement de l'objet

    void Start()
    {
        // Lancer le mouvement aléatoire dès le début de la scène
        StartRandomMovement();
    }

    void StartRandomMovement()
    {
        // Déterminer une direction aléatoire
        Vector3 randomDirection = Random.insideUnitSphere.normalized;
        randomDirection.y = 0f; // Ne pas inclure de déplacement vertical

        // Déplacer l'objet dans la direction aléatoire
        transform.Translate(randomDirection * speed * Time.deltaTime);
    }

    void Update()
    {
        // Si l'objet sort de l'écran, réinitialiser le mouvement aléatoire
        if (!GetComponent<Renderer>().isVisible)
        {
            StartRandomMovement();
        }
    }
}

