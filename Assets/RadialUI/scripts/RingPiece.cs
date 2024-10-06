using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Radial_UI
{
    public class RingPiece : MonoBehaviour
    {
        public Image bg_standar;
        public Image bg_selected;
        public Image bg_blocked;
        public Image icon_selected;
        public Image icon_standar;
        public Image icon_blocked;
        public bool blocked;
        private void Start()
        {
            
        }
        void CloseAll()
        {
            bg_selected.gameObject.SetActive(false);
            icon_selected.gameObject.SetActive(false);

            bg_standar.gameObject.SetActive(false);
            icon_standar.gameObject.SetActive(false);

            bg_blocked.gameObject.SetActive(false);
            icon_blocked.gameObject.SetActive(false);
        }
        public void OnActive()
        {
            CloseAll();
            if (!blocked)
            {
                bg_selected.gameObject.SetActive(true);
                icon_selected.gameObject.SetActive(true);
            }
            else
            {
                bg_blocked.gameObject.SetActive(true);
                icon_blocked.gameObject.SetActive(true);
            }

            transform.DOScale(0.37f, 0.3f).SetEase(Ease.OutQuad);
        }
        public void OnDesactive()
        {
            CloseAll();

            if (!blocked)
            {
                bg_standar.gameObject.SetActive(true);
                icon_standar.gameObject.SetActive(true);
            }
            else
            {
                bg_blocked.gameObject.SetActive(true);
                icon_blocked.gameObject.SetActive(true);
            }
          

            transform.DOScale(0.35f, 0.3f).SetEase(Ease.OutQuad);
        }
        Vector3 final_pos_bg;

        public void DoAnim(bool active,System.Action onComplete)
        {
            CloseAll();
            bg_standar.gameObject.SetActive(true);
            icon_standar.gameObject.SetActive(true);

            final_pos_bg = bg_standar.transform.localPosition;

            bg_standar.transform.localScale = Vector3.zero;

            bg_standar.transform.DOLocalMove(final_pos_bg, 0.3f).SetEase(Ease.OutQuad);

            bg_standar.transform.DOScale(1f, 0.3f).SetEase(Ease.OutQuad).OnComplete(() =>
            {
                icon_standar.gameObject.SetActive(true);
                if (active) onComplete?.Invoke();
            });
        }
    }

}
