using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // El objeto que la cámara debe seguir (jugador)
    public float smoothTime = 0.3f; // Tiempo para suavizar el movimiento
    public Vector3 offset; // Desplazamiento de la cámara respecto al jugador

    private Vector3 velocity = Vector3.zero; // Velocidad actual de la cámara (utilizada por SmoothDamp)

    void LateUpdate()
    {
        // La posición deseada es la del jugador más el offset
        Vector3 targetPosition = new Vector3(player.position.x + offset.x, player.position.y + offset.y, transform.position.z);

        // Usamos SmoothDamp para suavizar el movimiento de la cámara
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
