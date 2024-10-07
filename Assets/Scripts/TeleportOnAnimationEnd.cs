using UnityEngine;

public class TeleportOnAnimationEnd : MonoBehaviour
{
    public float minX = -4f; // M�nimo valor de X
    public float maxX = 4f;  // M�ximo valor de X

    // Funci�n que ser� llamada por el evento de la animaci�n
    public void Teleport()
    {
        // Generar una posici�n aleatoria en el eje X entre minX y maxX
        float randomX = Random.Range(minX, maxX);

        // Teletransportar el objeto a la nueva posici�n aleatoria
        Vector3 newPosition = new Vector3(randomX, transform.localPosition.y, transform.localPosition.z);
        transform.localPosition = newPosition;

        Debug.Log("Teletransportado a: " + newPosition);
    }
}