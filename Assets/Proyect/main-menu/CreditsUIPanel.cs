using System;
using UnityEngine;
using DG.Tweening;
public class CreditsUIPanel : UIPanel
{
    [Header("Credits")]
    public RectTransform rt_credits;

    public Vector3 init_position;
    public Vector3 end_position;
    Tween tween;
    bool isDead = false;
    public override void InitEnter()
    {
        tween.Kill();
        isDead = false;
        rt_credits.anchoredPosition = init_position;
    }

    public override void OnEnter(Action onComplete)
    {
        tween =rt_credits.DOLocalMoveY(end_position.y,20f).OnComplete(()=>{
            if(!isDead)
                MainMenuManager.instance.BackCreditsPanel();
        });
    }
    public override void OnExit(Action onComplete)
    {
        isDead = true;
        tween.Kill();
        this.gameObject.SetActive(false);
    }

}
