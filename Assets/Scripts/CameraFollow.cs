using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // El objeto que la cámara debe seguir (jugador)
    public float smoothTime = 0.3f; // Tiempo para suavizar el movimiento
    public Vector3 offset; // Desplazamiento de la cámara respecto al jugador

    private Vector3 velocity = Vector3.zero; // Velocidad actual de la cámara (utilizada por SmoothDamp)

    // Límites de la cámara
    public Vector2 minLimits; // Límite mínimo de la cámara (X e Y)
    public Vector2 maxLimits; // Límite máximo de la cámara (X e Y)

    void LateUpdate()
    {
        // Calculamos la posición objetivo del jugador más el offset
        Vector3 targetPosition = new Vector3(player.position.x + offset.x, player.position.y + offset.y, transform.position.z);

        // Usamos SmoothDamp para suavizar el movimiento de la cámara
        Vector3 smoothPosition = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

        // Aplicamos los límites a la posición suavizada
        smoothPosition.x = Mathf.Clamp(smoothPosition.x, minLimits.x, maxLimits.x);
        smoothPosition.y = Mathf.Clamp(smoothPosition.y, minLimits.y, maxLimits.y);

        // Asignamos la nueva posición con los límites aplicados
        transform.position = smoothPosition;
    }
}
