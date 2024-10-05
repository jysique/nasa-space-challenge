using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExoplanetInteact : MonoBehaviour
{
    public void OnZoom(float value)
    {
        float[] scales = { 0.2f, 0.3f, 0.4f, 0.5f };
        int scaleIndex = Mathf.Clamp((int)(value / 10), 0, scales.Length - 1);
        transform.localScale = new Vector3(scales[scaleIndex], scales[scaleIndex], scales[scaleIndex]);
    }
}
