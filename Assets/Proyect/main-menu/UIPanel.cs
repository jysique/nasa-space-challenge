using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPanel : MonoBehaviour
{
    public UIDopplerEffect[] effect;
    public GameObject[] particleOrbits;
    public float minDelay = 0.5f; // Tiempo mínimo de espera entre efectos
    public float maxDelay = 2f; // Tiempo máximo de espera entre efectos

    public CanvasGroup cg_panel;
    public CanvasGroup cg_sub_panel;

    
    float general_delay;
    public virtual void InitEnter()
    {
        foreach (var item in effect)
        {
            item.Init(0);
        }
        SetActiveParticles(false);
    }
    public virtual void InitExit()
    {
        foreach (var item in effect)
        {
            item.Init(1);
        }
        
    }
    public virtual void OnEnter(System.Action onComplete)
    {
        cg_panel.DOFade(1, 0.3f).OnComplete(() =>
        {
            StartDoppler(effect,1f, () =>
            {
                SetActiveParticles(true);
                onComplete?.Invoke();
               
            });
        });
      
    }

    public virtual void OnExit(System.Action onComplete)
    {

        StartDoppler( effect,2, () =>
        {
            SetActiveParticles(false);
            cg_panel.DOFade(0, 0.3f).OnComplete(() =>
            {
                onComplete?.Invoke();
            });
            
        });
    }
    void SetActiveParticles(bool active)
    {
        if (particleOrbits != null)
        {
            foreach (GameObject item in particleOrbits)
            {
                item.SetActive(active);
            }
        }
    }

    void StartDoppler(UIDopplerEffect[] e, float scale, System.Action onCompleted)
    {
        int index = 0;

        general_delay = 0;
        foreach (var item in e)
        {
            float initialDelay = Random.Range(minDelay, maxDelay);

            if (index == 0) initialDelay = 0f;
            general_delay += initialDelay;
            DOVirtual.DelayedCall(general_delay, () =>
            {
                item.StartDopplerEffect(scale);
                if (index == e.Length - 1)
                    onCompleted?.Invoke();
                index++;
            });

        }
    }
    //void OnCompletedOptionDoppler()
    //{
    //    cg_main_panel.gameObject.SetActive(true);
    //    cg_main_sub_panel.DOFade(0f, fade_time_panels).OnComplete(() =>
    //    {
    //        cg_main_panel.DOFade(0f, fade_time_panels).OnComplete(() =>
    //        {
    //            cg_main_panel.gameObject.SetActive(false);
    //            cg_option_panel.gameObject.SetActive(true);
    //            StartDoppler(particleOrbits2, effect2, false, null);
    //        });
    //        cg_main_panel.interactable = false;
    //    });
    //}
}
