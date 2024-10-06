using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleOrbit : MonoBehaviour
{
    public RectTransform pivot;
    public float orbitSpeed = 30f;
    public float radius = 100f;

    private float angle;

    void Start()
    {
        angle = 0f;
        UpdatePosition();
    }

    void Update()
    {
        angle += orbitSpeed * Time.deltaTime;

        angle %= 360f;

        UpdatePosition();
    }

    private void UpdatePosition()
    {
        float radianAngle = angle * Mathf.Deg2Rad; // Convertir a radianes
        Vector2 offset = new Vector2(Mathf.Cos(radianAngle), Mathf.Sin(radianAngle)) * radius;

        transform.position = pivot.position + (Vector3)offset;
    }
}
