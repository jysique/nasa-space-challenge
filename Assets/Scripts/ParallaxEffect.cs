using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    [SerializeField] private GameObject[] layers; // Array de objetos para el parallax
    [SerializeField] private float[] parallaxSpeeds; // Velocidades de desplazamiento de cada capa

    private Vector3 previousCameraPosition;

    void Start()
    {
        // Guardamos la posici�n inicial de la c�mara (o del objeto que controla el parallax)
        previousCameraPosition = transform.position;

        // Aseguramos que el array de velocidades tenga la misma longitud que el array de capas
        if (parallaxSpeeds.Length != layers.Length)
        {
            Debug.LogError("El n�mero de velocidades de parallax debe coincidir con el n�mero de capas.");
        }
    }

    void Update()
    {
        // Calculamos la diferencia de movimiento desde el �ltimo frame
        Vector3 deltaMovement = transform.position - previousCameraPosition;

        // Aplicamos el efecto de parallax a cada capa
        for (int i = 0; i < layers.Length; i++)
        {
            // Desplazamos la capa solo en el eje X, en la misma direcci�n que la c�mara, pero con la velocidad inversa (parallax)
            float parallaxAmount = parallaxSpeeds[i];
            float newLayerPositionX = layers[i].transform.position.x + deltaMovement.x * parallaxAmount;

            // Actualizamos solo la posici�n X de la capa, manteniendo las posiciones Y y Z originales
            layers[i].transform.position = new Vector3(newLayerPositionX, layers[i].transform.position.y, layers[i].transform.position.z);
        }

        // Actualizamos la posici�n previa de la c�mara para el siguiente frame
        previousCameraPosition = transform.position;
    }
}

