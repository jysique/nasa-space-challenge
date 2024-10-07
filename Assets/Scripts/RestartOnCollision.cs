using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class RestartOnCollision : MonoBehaviour
{
    [SerializeField] private GameObject fadeObject; // El objeto que hará el fade
    [SerializeField] private float fadeDuration = 1f; // Duración del fade

    private SpriteRenderer fadeCanvasGroup; // El CanvasGroup para controlar el alfa del objeto

    void Start()
    {
        // Aseguramos que el GameObject tenga un CanvasGroup para manejar la transparencia
        fadeCanvasGroup = fadeObject.GetComponent<SpriteRenderer>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Si el objeto con el que colisionamos es el jugador
        if (collision.gameObject.CompareTag("Player"))
        {
            // Iniciamos el fade
            StartFadeAndRestart();
        }
    }

    void StartFadeAndRestart()
    {
        // Subimos el alfa a 1 para hacer el fade in
        fadeCanvasGroup.DOFade(1f, fadeDuration).OnComplete(() =>
        {
            // Cuando el fade haya terminado, reiniciamos la escena
            RestartScene();
        });
    }

    void RestartScene()
    {
        // Reiniciamos la escena actual
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}