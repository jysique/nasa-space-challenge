using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Radial_UI
{
    public class RadialFollow : MonoBehaviour
    {
        public RectTransform uiElement;  // El men� o UI que queremos que siga al objeto
        public Transform target;         // El objeto que queremos seguir
        public Camera mainCamera;        // La c�mara desde donde se calcular�n las coordenadas de pantalla
        public Canvas canvas;            // El canvas donde se encuentra la UI
        public Vector3 offset;           // Offset para ajustar la posici�n en la UI

        void Update()
        {
            // Convertir la posici�n del objeto en coordenadas de pantalla
            Vector3 screenPos = mainCamera.WorldToScreenPoint(target.position + offset);

            // Si el objeto est� delante de la c�mara
            if (screenPos.z > 0)
            {
                Vector2 anchoredPos;
                RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    canvas.transform as RectTransform, screenPos, canvas.worldCamera, out anchoredPos);

                // Mover el UI al lugar correspondiente dentro del canvas
                uiElement.anchoredPosition = anchoredPos;
            }
        }
    }

}
