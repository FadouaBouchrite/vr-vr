using UnityEngine;
using System.Collections;

public class RandomInitialMovement : MonoBehaviour
{
    public float maxDistance = 0f; // Pas de limite de distance
    public float duration = 5f; // Durée du mouvement initial aléatoire

    private float elapsedTime = 0f;

    void Start()
    {
        StartCoroutine(RandomMove());
    }

    IEnumerator RandomMove()
    {
        // Mouvement initial aléatoire
        while (elapsedTime < duration)
        {
            // Générer une direction de déplacement aléatoire
            Vector3 randomDirection = Random.insideUnitSphere;

            // Ajouter une vitesse de déplacement
            float speed = 1f; // Ajustez la vitesse selon vos besoins
            // Appliquer le déplacement à l'atome
            transform.Translate(randomDirection * speed * Time.deltaTime);

            // Mettre à jour le temps écoulé
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        // Une fois que la durée est écoulée, arrêtez la coroutine
        StopCoroutine(RandomMove());
    }
}
