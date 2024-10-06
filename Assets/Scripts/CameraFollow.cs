using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // El objeto que la c�mara debe seguir (jugador)
    public float smoothTime = 0.3f; // Tiempo para suavizar el movimiento
    public Vector3 offset; // Desplazamiento de la c�mara respecto al jugador

    private Vector3 velocity = Vector3.zero; // Velocidad actual de la c�mara (utilizada por SmoothDamp)

    void LateUpdate()
    {
        // La posici�n deseada es la del jugador m�s el offset
        Vector3 targetPosition = new Vector3(player.position.x + offset.x, player.position.y + offset.y, transform.position.z);

        // Usamos SmoothDamp para suavizar el movimiento de la c�mara
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
