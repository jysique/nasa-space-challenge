using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDopplerEffect : MonoBehaviour
{
    Image targetImage; // La imagen a la que le aplicaremos el efecto Doppler
    public float duration = 1f; // Duración del efecto Doppler
   
    void Start()
    {
        targetImage = GetComponent<Image>();
    }

    bool in_doppler = false;
    public void Init(float initscale)
    {
        targetImage.transform.localScale = Vector3.one * initscale;
    }
    public void StartDopplerEffect(float scaleTo)
    {
        if (in_doppler) return;
        //float scale = inside ? minScale : maxScale;
        in_doppler = true;
       
        targetImage.transform.DOScale(scaleTo, duration / 2f).SetEase(Ease.OutQuad).OnComplete(() =>
        {
            in_doppler = false;
//            if(inside)
//                gameObject.SetActive(false);
        });
    }
}
