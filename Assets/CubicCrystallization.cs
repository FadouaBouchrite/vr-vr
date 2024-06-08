using UnityEngine;
using System.Collections;

public class CubicCrystallization : MonoBehaviour
{
    public float moveSpeed = 1f;
    public Transform sphere1;
    public Transform sphere2;
    public Transform sphere3;
    public Transform sphere4;
    public Transform sphere5;
    public Transform sphere6;
    public Transform sphere7;
    public Transform sphere8;
    public Transform sphere9;
    public Transform sphere10;
    public Transform sphere11;
    public Transform sphere12;
    public Transform sphere13;
    public Transform sphere14;

    // Variable pour stocker le cube à déplacer
    public GameObject cubeToMove;

    void Start()
    {
        // Attendre que le mouvement initial aléatoire soit terminé
        StartCoroutine(MoveAtomsAfterRandomMove());
    }

    IEnumerator MoveAtomsAfterRandomMove()
    {
        // Attendre un court instant pour s'assurer que le mouvement initial aléatoire est terminé
        yield return new WaitForSeconds(5f);

        // Déplacer chaque sphère vers sa position cible lentement
        StartCoroutine(MoveSphereSlowly(sphere1, new Vector3(-9.76f, 0.55f, 19.37372f)));
        StartCoroutine(MoveSphereSlowly(sphere2, new Vector3(-12.74f, 0.55f, 19.37372f)));
        StartCoroutine(MoveSphereSlowly(sphere3, new Vector3(-12.68f, 3.48f, 19.3f)));
        StartCoroutine(MoveSphereSlowly(sphere4, new Vector3(-9.68f, 3.48f, 19.3f)));
        StartCoroutine(MoveSphereSlowly(sphere5, new Vector3(-11.02f, 1.85f, 19.37372f)));
        StartCoroutine(MoveSphereSlowly(sphere6, new Vector3(-9.76f, 1.77f, 21.02f)));
        StartCoroutine(MoveSphereSlowly(sphere7, new Vector3(-9.68f, 3.44f, 22.49f)));
        StartCoroutine(MoveSphereSlowly(sphere8, new Vector3(-9.76f, 0.5100001f, 22.56372f)));
        StartCoroutine(MoveSphereSlowly(sphere9, new Vector3(-12.74f, 0.55f, 22.62f)));
        StartCoroutine(MoveSphereSlowly(sphere10, new Vector3(-11.02f, 1.81f, 22.56372f)));
        StartCoroutine(MoveSphereSlowly(sphere11, new Vector3(-11.14f, 0.55f, 20.94f)));
        StartCoroutine(MoveSphereSlowly(sphere12, new Vector3(-12.74f, 3.43f, 22.62f)));
        StartCoroutine(MoveSphereSlowly(sphere13, new Vector3(-12.82f, 1.77f, 20.94f)));
        StartCoroutine(MoveSphereSlowly(sphere14, new Vector3(-12.82f, 1.77f, 20.94f)));

        // Attendre que le mouvement soit terminé avant de former le réseau CFC
        yield return new WaitForSeconds(moveSpeed * 14f);

        // Former le réseau CFC
        FormCFCNetwork();

        // Attendre un peu avant de former le cube
        yield return new WaitForSeconds(1f);

        // Rapprocher les atomes pour former une seule unité
     

        // Déplacer le cube spécifié
        StartCoroutine(MoveCube(cubeToMove, new Vector3(-9.18f, 3.32f, 22.5896f)));
    }

    void FormCFCNetwork()
    {
        // Longueur du côté du CFC (distance entre les sommets)
        float sideLength = Vector3.Distance(sphere1.position, sphere2.position);

        // Décalages pour la duplication dans différentes directions
        Vector3 leftOffset = new Vector3(-2f * sideLength, 0f, 0f);
        Vector3 rightOffset = new Vector3(2f * sideLength, 0f, 0f);
        Vector3 upOffset = new Vector3(0f, 2f * sideLength, 0f);
        Vector3 downOffset = new Vector3(0f, -2f * sideLength, 0f);
        Vector3 forwardOffset = new Vector3(0f, 0f, 2f * sideLength);
        Vector3 backwardOffset = new Vector3(0f, 0f, -2f * sideLength);

        // Dupliquer le CFC dans différentes directions pour former le réseau CFC
        // (code de duplication du CFC ici...)
    }

    IEnumerator MoveSphereSlowly(Transform sphere, Vector3 targetPosition)
    {
        float journeyLength = Vector3.Distance(sphere.position, targetPosition);
        float startTime = Time.time;

        while (Time.time < startTime + moveSpeed)
        {
            float distCovered = (Time.time - startTime) * moveSpeed;
            float fractionOfJourney = distCovered / journeyLength;
            sphere.position = Vector3.Lerp(sphere.position, targetPosition, fractionOfJourney);
            yield return null;
        }

        // Affecter la position cible à la sphère
        sphere.position = targetPosition;
    }

    IEnumerator MoveAtomsTogether()
    {
        // Créer un GameObject vide comme parent pour toutes les sphères
        GameObject sphereParent = new GameObject("SphereParent");
        sphereParent.transform.position = Vector3.zero;

        // Assigner les sphères comme enfants du GameObject parent
        sphere1.SetParent(sphereParent.transform);
        sphere2.SetParent(sphereParent.transform);
        sphere3.SetParent(sphereParent.transform);
        sphere4.SetParent(sphereParent.transform);
        sphere5.SetParent(sphereParent.transform);
        sphere6.SetParent(sphereParent.transform);
        sphere7.SetParent(sphereParent.transform);
        sphere8.SetParent(sphereParent.transform);
        sphere9.SetParent(sphereParent.transform);
        sphere10.SetParent(sphereParent.transform);
        sphere11.SetParent(sphereParent.transform);
        sphere12.SetParent(sphereParent.transform);
        sphere13.SetParent(sphereParent.transform);
        sphere14.SetParent(sphereParent.transform);

        // Attendre un moment pour que les sphères atteignent leur position cible
        yield return new WaitForSeconds(moveSpeed);

        // Calculer les dimensions du cube englobant
        Bounds bounds = new Bounds(sphereParent.transform.position, Vector3.zero);
        foreach (Renderer renderer in sphereParent.GetComponentsInChildren<Renderer>())
        {
            bounds.Encapsulate(renderer.bounds);
        }

        // Augmenter les dimensions du cube englobant
        float scaleMultiplier = 3.88f; // Facteur d'échelle
        Vector3 cubeSize = bounds.size * scaleMultiplier;

        // Créer un cube englobant toutes les sphères
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.position = bounds.center;

        // Assigner la taille augmentée au cube
        cube.transform.localScale = Vector3.zero; // Définir la taille initiale à zéro pour le rendre invisible

        // Faire apparaître le cube progressivement
        float duration = 2f; // Durée de l'animation d'apparition
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            cube.transform.localScale = Vector3.Lerp(Vector3.zero, cubeSize, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Assurer que le cube soit exactement à la taille finale
        cube.transform.localScale = cubeSize;

        // Attendre un moment pour observer le résultat
        yield return new WaitForSeconds(2f);

        // Faire tourner le cube de 360 degrés
        StartCoroutine(RotateCube(cube, 360f, 2f));
    }

    IEnumerator MoveCube(GameObject cube, Vector3 targetPosition)
    {
        float journeyLength = Vector3.Distance(cube.transform.position, targetPosition);
        float startTime = Time.time;

        while (Time.time < startTime + moveSpeed)
        {
            float distCovered = (Time.time - startTime) * moveSpeed;
            float fractionOfJourney = distCovered / journeyLength;
            cube.transform.position = Vector3.Lerp(cube.transform.position, targetPosition, fractionOfJourney);
            yield return null;
        }

        // Affecter la position cible au cube
        cube.transform.position = targetPosition;
    }

    IEnumerator RotateCube(GameObject cube, float angle, float duration)
{
    Quaternion startRotation = cube.transform.rotation;
    Quaternion endRotation = Quaternion.Euler(0, angle, 0) * startRotation;

    float startTime = Time.time;
    while (Time.time < startTime + duration)
    {
        float t = (Time.time - startTime) / duration;
        cube.transform.rotation = Quaternion.Slerp(startRotation, endRotation, t);
        yield return null;
    }

    // Assurer que le cube soit exactement à l'angle final
    cube.transform.rotation = endRotation;
}


}
