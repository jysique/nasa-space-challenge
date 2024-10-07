using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Radial_UI
{
    public class RadialFollow : MonoBehaviour
    {
        public Camera uiCamera;
        private RectTransform rt;
        public Transform target;
        public Canvas canvas;
        public Vector3 gap;
         RadialMenu radialMenu;
        private void Awake()
        {
            rt = GetComponent<RectTransform>();
            radialMenu = GetComponent<RadialMenu>();
        }
        private void OnEnable()
        {
            PositionRectTransform();
        }
        public void PositionRectTransform()
        {
            Time.timeScale = 0.01f;
            Debug.Log(" position");
            radialMenu.Anim();
            // Convertir la posición del objeto en coordenadas de pantalla
            Vector3 screenPos = uiCamera.WorldToScreenPoint(target.position + gap);

            // Si el objeto está delante de la cámara
            if (screenPos.z > 0)
            {
                Vector2 anchoredPos;
                RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    canvas.transform as RectTransform, screenPos, canvas.worldCamera, out anchoredPos);

                // Mover el UI al lugar correspondiente dentro del canvas
                rt.anchoredPosition = anchoredPos;
            }
        }
        public Vector3 GetWorldPositionFromRect(RectTransform rt)
        {
            Vector3[] worldCorners = new Vector3[4];
            rt.GetWorldCorners(worldCorners);
            return worldCorners[0];
        }
    }

}
