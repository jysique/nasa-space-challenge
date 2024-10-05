using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadialFollow : MonoBehaviour
{
    private RectTransform rectTransformToMove;
    public Transform target; 
    private void Awake()
    {
        rectTransformToMove = GetComponent<RectTransform>();
    }
    private void OnEnable()
    {
        PositionRectTransform();
    }
    public void PositionRectTransform()
    {
        if (target == null) return;
        Vector3 worldPosition = target.position;

        rectTransformToMove.position = worldPosition;
        rectTransformToMove.anchorMin = new Vector2(0.5f, 0.5f);
        rectTransformToMove.anchorMax = new Vector2(0.5f, 0.5f);
        rectTransformToMove.pivot = new Vector2(0.5f, 0.5f);
    }
}
