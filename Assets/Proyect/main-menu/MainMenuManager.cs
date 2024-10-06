using DG.Tweening;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    
    public UIDopplerEffect[] effect;
    public UIDopplerEffect[] effect2;
    public ParticleOrbit[] particleOrbits;
    public ParticleOrbit[] particleOrbits2;
    public float minDelay = 0.5f; // Tiempo mínimo de espera entre efectos
    public float maxDelay = 2f; // Tiempo máximo de espera entre efectos
    public CanvasGroup cg_main_panel;
    public CanvasGroup cg_main_sub_panel;
    

    private void Update()
    {
    }
    public void OnClick(string id)
    {
        switch (id)
        {
            case "menu":
                StartDoppler(particleOrbits, effect,true, OnCompletedDoppler1);
                break;
            case "quit":
                break;
            case "play":
                break;
        }
    }
    public void Animation2()
    {

    }
    float general_delay;
    void StartDoppler(ParticleOrbit[] po,UIDopplerEffect[] e, bool inside, System.Action onCompleted)
    {
        int index = 0;
        if(po!= null)
        {
            foreach (ParticleOrbit item in po)
            {
                item.gameObject.SetActive(false);
            }
        }
        general_delay = 0;
        foreach (var item in e)
        {

            float initialDelay = Random.Range(minDelay, maxDelay);
            
            if (index == 0) initialDelay = 0f;
            general_delay += initialDelay;
            DOVirtual.DelayedCall(general_delay, () =>
            {
                item.StartDopplerEffect(inside);
                if (index == e.Length-1)
                    onCompleted?.Invoke();
                index++;
            });
            
        }
    }
    void OnCompletedDoppler1()
    {
        cg_main_sub_panel.DOFade(0f, 0.3f).OnComplete(() =>
        {
            cg_main_panel.DOFade(0f, 0.3f).OnComplete(() =>
            {
                StartDoppler(particleOrbits2, effect2, false, null);
            });
            cg_main_panel.interactable = false;
        });
    }
}
