using UnityEngine;
using TMPro;  // Para trabajar con TextMeshPro
using DG.Tweening;  // Para utilizar DOTween

public class InfoPoint : MonoBehaviour
{
    [Header("References")]
    public CanvasGroup canvasGroup;  // El CanvasGroup para hacer el fade
    public Transform infoObject;  // El objeto cuya escala será tweakeada
    public TMP_Text infoText;  // El componente TextMeshPro donde se mostrará el texto

    [Header("Info Settings")]
    public string info;  // Texto que aparecerá al entrar en el InfoPoint

    [Header("Tween Settings")]
    public float fadeDuration = 0.5f;  // Duración del fade in/out
    public float scaleDuration = 2f;   // Duración del ciclo de scale
    public float scaleAmount = 1.05f;  // Cantidad de aumento en la escala
    private Tween scaleTween;  // Para almacenar la animación de escala

    public bool collected = false;

    private void Start()
    {
        // Inicializar el canvasGroup como invisible
        canvasGroup.alpha = 0f;

        // Configurar el loop sutil de la escala
        StartScaleLoop();
    }

    private void StartScaleLoop()
    {
        // Hacer que el objeto infoObject tenga un scale loop sutil
        scaleTween = infoObject.DOScale(scaleAmount, scaleDuration)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine);
    }
 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Actualizar el texto del infoText con el valor de info
            infoText.text = info;

            // Hacer fade in del CanvasGroup
            canvasGroup.DOFade(1f, fadeDuration);
            if (!collected)
            {
                collected = true;
                WinCondition.instance.AddCounter();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Hacer fade out del CanvasGroup
            canvasGroup.DOFade(0f, fadeDuration);
        }
    }

    private void OnDestroy()
    {
        // Asegurarse de que el tween se detenga cuando se destruya el objeto
        if (scaleTween != null)
        {
            scaleTween.Kill();
        }
    }
}
