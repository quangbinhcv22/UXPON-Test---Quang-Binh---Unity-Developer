using DG.Tweening;
using UnityEngine;

namespace View
{
    public class ProgressBar_Move : ProgressBar
    {
        [Space, SerializeField] private RectTransform _parent;
        private RectTransform _transform;


        private void Awake()
        {
            _transform = fill.GetComponent<RectTransform>();
        }

        protected override Tween Tweening(float value)
        {
            return _transform.DOLocalMoveX((value - 1) * _parent.sizeDelta.x, duration).SetEase(ease);
        }

        public override void SetFillImmediately(float value)
        {
#if UNITY_EDITOR
            _transform = fill.GetComponent<RectTransform>();
#endif
            var position = _transform.localPosition;
            position.x = (value - 1) * _parent.sizeDelta.x;
            
            _transform.localPosition = position;
        }
    }
}