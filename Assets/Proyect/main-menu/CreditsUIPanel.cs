using System;
using UnityEngine;
using DG.Tweening;
public class CreditsUIPanel : UIPanel
{
    [Header("Credits")]
    public RectTransform rt_credits;

    public Vector3 init_position;
    public Vector3 end_position;
    public override void InitEnter()
    {
        rt_credits.anchoredPosition = init_position;
    }

    public override void OnEnter(Action onComplete)
    {
        rt_credits.DOMoveY(end_position.y,200f).OnComplete(()=>{
            Debug.Log(" on complet");
            MainMenuManager.instance.BackCreditsPanel();
        });
    }
    public override void OnExit(Action onComplete)
    {
        this.gameObject.SetActive(false);
    }

}
