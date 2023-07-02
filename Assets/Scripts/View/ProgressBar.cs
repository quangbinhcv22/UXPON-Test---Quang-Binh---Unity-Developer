using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class ProgressBar : MonoBehaviour
    {
        [SerializeField] protected Image fill;
        [SerializeField] protected float duration = 0.25f;
        [SerializeField] protected Ease ease = Ease.OutQuad;

        private Tween _lastTween;

        public void SetFill(float current, float max)
        {
            var value = max == 0 ? 0 : current / max;
            SetFill(value);
        }

        public virtual void SetFillImmediately(float value)
        {
            fill.fillAmount = value;
        }

        public void SetFill(float value)
        {
            _lastTween?.Kill();
            _lastTween = Tweening(value);
        }

        protected virtual Tween Tweening(float value)
        {
            return fill.DOFillAmount(value, duration).SetEase(ease);
        }
    }
}