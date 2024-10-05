using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadialMenu : MonoBehaviour
{
    public int items;
    public RingPiece radial_fab;
    public RingPiece[] radial_rings;

    private float degressPerPiece;
    private float gapDegress = 1f;

    private void Start()
    {
        degressPerPiece = 360f / items;
        float distanceToIcon = Vector3.Distance(radial_fab.icon.transform.position, radial_fab.background.transform.position);
        radial_rings = new RingPiece[items];
        for (int i = 0; i < items; i++)
        {
            radial_rings[i] = Instantiate(radial_fab, transform);
            radial_rings[i].background.fillAmount = (1f / items) - (gapDegress / 360);
            radial_rings[i].background.transform.localRotation = Quaternion.Euler(0, 0, degressPerPiece / 2f + gapDegress / 2f + i * degressPerPiece);

            Vector3 directionVector = Quaternion.AngleAxis(i * degressPerPiece, Vector3.forward) * Vector3.up;
            Vector3 movementVector = directionVector * distanceToIcon *2;
            radial_rings[i].icon.transform.localPosition = radial_rings[i].background.transform.localPosition + movementVector;
        }
    }
    private void Update()
    {
        int activeElement = GetActiveElement();
        HighLightActiveElement(activeElement);
        
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
        if(Input.GetMouseButtonDown(0)) { 

        }
    }

    void HighLightActiveElement(int activeElement)
    {
        for (int i = 0; i < radial_rings.Length; i++)
        {
            if(i == activeElement)
            {
                radial_rings[i].background.color = new Color(1f,1f, 1f, 0.75f);
            }
            else
            {
                radial_rings[i].background.color = new Color(1f, 1f, 1f, 0.5f);
            }
        }
    }
    float NormalizeAngle(float a) => (a + 360f) % 360f;
}
