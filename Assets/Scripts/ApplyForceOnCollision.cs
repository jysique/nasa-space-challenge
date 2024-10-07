using UnityEngine;

public class ApplyForceOnCollision : MonoBehaviour
{
    // Magnitud de la fuerza que se aplicará hacia arriba
    public float forceUp = 10f;

    // Detectar la colisión con el jugador
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Verificar si el objeto con el que se colisiona es el jugador
        if (collision.gameObject.CompareTag("Player"))
        {
            // Obtener el Rigidbody2D del jugador
            Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();

            if (playerRb != null)
            {
                // Aplicar una fuerza hacia arriba
                playerRb.AddForce(Vector2.up * forceUp, ForceMode2D.Impulse);
            }
        }
    }
}