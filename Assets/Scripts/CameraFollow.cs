using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // El objeto que la c�mara debe seguir (jugador)
    public float smoothTime = 0.3f; // Tiempo para suavizar el movimiento
    public Vector3 offset; // Desplazamiento de la c�mara respecto al jugador

    private Vector3 velocity = Vector3.zero; // Velocidad actual de la c�mara (utilizada por SmoothDamp)

    // L�mites de la c�mara
    public Vector2 minLimits; // L�mite m�nimo de la c�mara (X e Y)
    public Vector2 maxLimits; // L�mite m�ximo de la c�mara (X e Y)

    void LateUpdate()
    {
        // Calculamos la posici�n objetivo del jugador m�s el offset
        Vector3 targetPosition = new Vector3(player.position.x + offset.x, player.position.y + offset.y, transform.position.z);

        // Usamos SmoothDamp para suavizar el movimiento de la c�mara
        Vector3 smoothPosition = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

        // Aplicamos los l�mites a la posici�n suavizada
        smoothPosition.x = Mathf.Clamp(smoothPosition.x, minLimits.x, maxLimits.x);
        smoothPosition.y = Mathf.Clamp(smoothPosition.y, minLimits.y, maxLimits.y);

        // Asignamos la nueva posici�n con los l�mites aplicados
        transform.position = smoothPosition;
    }
}
