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
        public Vector3 gap;
        public RadialMenu radialMenu;
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
            radialMenu.Anim();
            Vector3 center = new Vector3(433, 270, 0);
            Vector3 screenPosition = uiCamera.WorldToScreenPoint(target.position);
            rt.localPosition = screenPosition - center + gap;
        }
        public Vector3 GetWorldPositionFromRect(RectTransform rt)
        {
            Vector3[] worldCorners = new Vector3[4];
            rt.GetWorldCorners(worldCorners);
            return worldCorners[0];
        }
    }

}
