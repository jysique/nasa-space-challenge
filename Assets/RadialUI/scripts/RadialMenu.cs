using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Radial_UI
{
    
    public abstract class RadialAction : ScriptableObject
    {
        public string id;
        public void Do()
        {
            Debug.Log(" click in " + id);
        }
    }

    public class RadialMenu : MonoBehaviour
    {
        public RadialAction[] radialActions;
        public RingPiece radial_fab;
        public RingPiece[] radial_rings;

        private float degressPerPiece;
        private float gapDegress = 1f;
        public float radius { get; private set; }
        CanvasGroup cg;
        bool active;
        private void Awake()
        {
            cg = GetComponent<CanvasGroup>();
        }
        private void Start()
        {

            degressPerPiece = 360f / radialActions.Length;

            float distanceToIcon = Vector3.Distance(radial_fab.icon.transform.position, radial_fab.background.transform.position);
            
            radial_rings = new RingPiece[radialActions.Length];
            for (int i = 0; i < radialActions.Length; i++)
            {
                radial_rings[i] = Instantiate(radial_fab, transform);
                radial_rings[i].background.fillAmount = (1f / radialActions.Length) - (gapDegress / 360);
                radial_rings[i].background.transform.localRotation = Quaternion.Euler(0, 0, degressPerPiece / 2f + gapDegress / 2f + i * degressPerPiece);
                radial_rings[i].OnActive();
                radial_rings[i].DoAnim(i == radialActions.Length-1, ActiveMenu);

                Vector3 directionVector = Quaternion.AngleAxis(i * degressPerPiece, Vector3.forward) * Vector3.up;
                Vector3 movementVector = directionVector * distanceToIcon * 2;

                //  radius = Vector3.Distance(this.transform.position, movementVector);
                radial_rings[i].icon.transform.localPosition = radial_rings[i].background.transform.localPosition + movementVector;
            }
        }
        
        public void Anim() {
            cg.interactable = false;
            for (int i = 0; i < radial_rings.Length; i++)
            {
                radial_rings[i].DoAnim(i == radialActions.Length - 1,ActiveMenu);
            }
        }
        void ActiveMenu()
        {
            active = true;
            cg.interactable = true;
        }
        private void Update()
        {
            if (!active) return;
            int activeElement = GetActiveElement();

            HighLightActiveElement(activeElement);
            RespondToMouseInput(activeElement);
        }
        int GetActiveElement()
        {
            //Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2);
            Vector3 screenCenter = this.transform.position;
            Vector3 cursosVector = Input.mousePosition - screenCenter;

            float mouseAngle = Vector3.SignedAngle(Vector3.up, cursosVector, Vector3.forward) + degressPerPiece / 2f;
            float normalizedMouseAngle = NormalizeAngle(mouseAngle);
            return (int)(normalizedMouseAngle / degressPerPiece);
        }
        void RespondToMouseInput(int activeElement)
        {
            if (Input.GetMouseButtonDown(0))
            {
                radialActions[activeElement].Do();
            }
        }

        void HighLightActiveElement(int activeElement)
        {
            for (int i = 0; i < radial_rings.Length; i++)
            {
                if (i == activeElement)
                {
                    radial_rings[i].OnActive();
                }
                else
                {
                    radial_rings[i].OnDesactive();
                }
            }
        }
        float NormalizeAngle(float a) => (a + 360f) % 360f;
    }

}
