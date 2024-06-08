using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bougebutton : MonoBehaviour
{
    public float moveSpeed = 1f; // Vitesse de déplacement du bouton
    public float moveDistance = 1f; // Distance maximale de déplacement du bouton

    private Vector3 startPosition; // Position initiale du bouton

    void Start()
    {
        // Stocker la position initiale du bouton
        startPosition = transform.position;
    }

    void Update()
    {
        // Générer une direction de déplacement aléatoire
        Vector3 randomDirection = Random.insideUnitCircle.normalized;

        // Calculer la position cible
        Vector3 targetPosition = startPosition + randomDirection * moveDistance;

        // Déplacer le bouton vers la position cible
        transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);
    }
}