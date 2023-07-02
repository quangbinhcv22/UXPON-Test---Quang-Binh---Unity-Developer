using DG.Tweening;
using Entities;
using Pattern;
using TMPro;
using UnityEngine;

namespace View
{
    public class FloatDamageText : MonoBehaviour
    {
        public static void ShowAt(float damage, DamageType type, Vector3 position)
        {
            Pooler.Get($"text_damage_{type.ToString().ToLower()}", t => t.GetComponent<FloatDamageText>().Show(damage, position));
        }

        [SerializeField] private CanvasGroup group;
        [SerializeField] private TMP_Text text;

        [Space, SerializeField] private float yTarget = 0.25f;
        [SerializeField] private float floatDuration = 0.25f;
        [SerializeField] private Ease ease = Ease.OutQuad;

        [Space, SerializeField] private float alphaDuration = 0.25f;


        public void Show(float damage, Vector3 position)
        {
            text.SetText($"-{damage:N0}");
            group.alpha = 1f;

            transform.position = position;
            transform.DOMoveY(position.y + yTarget, floatDuration).SetEase(ease).onComplete += () =>
            {
                group.DOFade(0f, alphaDuration).SetEase(ease).onComplete += () => gameObject.SetActive(false);
            };
        }
    }
}