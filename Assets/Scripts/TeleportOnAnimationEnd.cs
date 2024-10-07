using UnityEngine;

public class TeleportOnAnimationEnd : MonoBehaviour
{
    public float minX = -4f; // Mínimo valor de X
    public float maxX = 4f;  // Máximo valor de X

    // Función que será llamada por el evento de la animación
    public void Teleport()
    {
        // Generar una posición aleatoria en el eje X entre minX y maxX
        float randomX = Random.Range(minX, maxX);

        // Teletransportar el objeto a la nueva posición aleatoria
        Vector3 newPosition = new Vector3(randomX, transform.localPosition.y, transform.localPosition.z);
        transform.localPosition = newPosition;

        Debug.Log("Teletransportado a: " + newPosition);
    }
}