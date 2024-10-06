using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    
    public UIDopplerEffect[] effect;
    public ParticleOrbit[] particleOrbits;
    public float minDelay = 0.5f; // Tiempo mínimo de espera entre efectos
    public float maxDelay = 2f; // Tiempo máximo de espera entre efectos
    public CanvasGroup cg_panel_1;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            Animation1();
        }
    }

    public void Animation1()
    {
        foreach (ParticleOrbit item in particleOrbits)
        {
            
        }

        foreach (var item in effect)
        {
            float initialDelay = Random.Range(minDelay, maxDelay);

            DOVirtual.DelayedCall(initialDelay, () =>
            {
                item.StartDopplerEffect(true);
                cg_panel_1.DOFade(0f, 0.3f);
            });
        }
    }
}
