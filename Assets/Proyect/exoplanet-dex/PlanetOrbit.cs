using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetOrbit : MonoBehaviour
{
    private Transform starTransform; // La estrella alrededor de la cual orbita el planeta
    private float orbitRadius;
    private float orbitAngle;
    private float orbitSpeed;

    public void InitializeOrbit(Transform star, float radius, float angle)
    {
        starTransform = star;
        orbitRadius = radius;
        orbitAngle = angle;
        orbitSpeed = Random.Range(0.1f, 0.5f); // Velocidad aleatoria para cada planeta
    }

    void Update()
    {
        // Incrementar el ángulo para mover el planeta a lo largo de su órbita
        orbitAngle += orbitSpeed * Time.deltaTime;

        // Calcular la nueva posición del planeta
        Vector3 orbitPosition = Vector3.zero;
        if (starTransform != null)
        {
            orbitPosition = new Vector3(
            starTransform.position.x + Mathf.Cos(orbitAngle * Mathf.Deg2Rad) * orbitRadius,
            starTransform.position.y + Mathf.Sin(orbitAngle * Mathf.Deg2Rad) * orbitRadius,
            0);
        }
            
        transform.position = orbitPosition;
    }
}
