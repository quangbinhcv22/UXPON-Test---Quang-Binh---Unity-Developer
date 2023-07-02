using Entities;
using UnityEngine;

namespace View
{
    public class HpBar : MonoBehaviour
    {
        [SerializeField] private ProgressBar_Move bar;
        
        private Character _owner;

        private void Awake()
        {
            _owner = GetComponentInParent<Character>();

            _owner.onHpChanged += UpdateFill;
            UpdateFill();
        }

        public void UpdateFill()
        {
            bar.SetFill(_owner.HpCurrent, _owner.hpMax);
        }
    }
}