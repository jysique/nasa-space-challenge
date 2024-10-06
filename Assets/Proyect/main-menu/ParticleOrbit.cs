using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleOrbit : MonoBehaviour
{
    public RectTransform pivot;
    public float orbitSpeed = 30f;
    public float radius = 100f;
    public float startAngle = 0;
    public bool hack;
    private float angle;

    void Start()
    {
        angle = startAngle;
        UpdatePosition();
    }

    void Update()
    {
        angle += orbitSpeed * Time.deltaTime;
        angle %= 360f;

        if (hack)
        {
            // 90
            if(angle > 60 && angle < 120)
            {
                angle = 121;
            }
            //270
            if (angle > 240 && angle < 300)
            {
                angle = 301;
            }
        }

        UpdatePosition();
    }

    private void UpdatePosition()
    {
        float radianAngle = angle * Mathf.Deg2Rad; // Convertir a radianes
        Vector2 offset = new Vector2(Mathf.Cos(radianAngle), Mathf.Sin(radianAngle)) * radius;

        transform.position = pivot.position + (Vector3)offset;
    }
}
