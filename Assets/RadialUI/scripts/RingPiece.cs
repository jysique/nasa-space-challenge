using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Radial_UI
{
    public class RingPiece : MonoBehaviour
    {
        public Image background;
        public Image icon;
        public void OnActive()
        {
            transform.DOScale(0.37f, 0.3f).SetEase(Ease.OutQuad);
            background.color = new Color(1f, 1f, 1f, 0.75f);
        }
        public void OnDesactive()
        {
            transform.DOScale(0.35f, 0.3f).SetEase(Ease.OutQuad);
            background.color = new Color(1f, 1f, 1f, 0.5f);
        }
        public void DoAnim(bool active,System.Action onComplete)
        {
            background.transform.localScale = Vector3.zero;
            icon.gameObject.SetActive(false);
            background.transform.DOScale(1f, 0.3f).SetEase(Ease.OutQuad).OnComplete(() =>
            {
                icon.gameObject.SetActive(true);
                if (active) onComplete?.Invoke();
            });
        }
    }

}
