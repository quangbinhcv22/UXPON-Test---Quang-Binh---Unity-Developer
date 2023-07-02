using System;
using DG.Tweening;
using UnityEngine;
using View;

namespace Entities
{
    [RequireComponent(typeof(Rigidbody))]
    public class Character : MonoBehaviour
    {
        public static readonly int HashDie = Animator.StringToHash("die");
        public static readonly int HashGetHit = Animator.StringToHash("get_hit");
        public static readonly int HashSpeed = Animator.StringToHash("speed");
        public static readonly int HashAttack = Animator.StringToHash("attack");


        [Header("Stats")] public int hpMax = 3;
        public float speed = 1;
        public int damage = 1;
        public DamageType damageType;

        [Header("Audio")] [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip dieSfx;
        [SerializeField] private AudioClip attackSfx;

        [Header("Effects")] [SerializeField] private ParticleSystem dieVfx;
        [SerializeField] private ParticleSystem appearVfx;
        [SerializeField] private ParticleSystem attackVfx;

        [Space] public Transform model;
        public Animator animator;

        [NonSerialized] public Rigidbody Rb;
        [NonSerialized] private Collider _collider;


        private int _hpCurrent = 1;

        public int HpCurrent
        {
            get => _hpCurrent;
            set
            {
                _hpCurrent = value;

                onHpChanged?.Invoke();
                if (_hpCurrent <= 0) OnDie();
            }
        }

        public Action onHpChanged;
        public Action onDie;


        protected virtual void Awake()
        {
            Rb = GetComponent<Rigidbody>();
            _collider = GetComponent<Collider>();
        }

        protected virtual void OnEnable()
        {
            OnStartGame();
            Vfx.PlayOneShot(appearVfx, transform, 0.15f);
        }

        public virtual void OnStartGame()
        {
            HpCurrent = hpMax;

            _collider.enabled = true;
            animator.SetBool(HashDie, false);
        }

        public void TakeDamage(int dmg, DamageType type)
        {
            HpCurrent -= dmg;
            animator.SetTrigger(HashGetHit);

            FloatDamageText.ShowAt(dmg, type, transform.position);
        }

        public void OnDie()
        {
            onDie?.Invoke();

            Rb.velocity = Vector3.zero;
            _collider.enabled = false;

            animator.SetBool(HashDie, true);

            audioSource.PlayOneShot(dieSfx);
            Vfx.PlayOneShot(dieVfx, transform);
        }

        public void OnDieDone()
        {
            gameObject.SetActive(false);
        }


        [NonSerialized] public Character Target;
        [NonSerialized] public bool IsAttacking;

        public void DoAttack(Character target)
        {
            if (IsAttacking) return;

            this.Target = target;
            IsAttacking = true;

            animator.SetBool(HashAttack, IsAttacking);
            audioSource.PlayOneShot(attackSfx);
        }

        public void OnDoAttack()
        {
            if (!Target) return;

            Target.TakeDamage(damage, damageType);
            Target.KnockBack(transform);

            Vfx.PlayOneShot(attackVfx, Target.transform);
        }
        
        public void OnEndAttack()
        {
            IsAttacking = false;
            animator.SetBool(HashAttack, IsAttacking);

            if (!Target) return;
        }


        private void OnCollisionStay(Collision collision)
        {
            if (IsAttacking) return;

            if (collision.gameObject.TryGetComponent<Character>(out var target))
            {
                DoAttack(target);
            }
        }

        public void KnockBack(Transform source)
        {
            var position = transform.position;
            var sourcePosition = new Vector3(source.position.x, position.y, source.position.z);

            transform.DOMove(position + (position - sourcePosition).normalized * 1.5f, 0.15f);
        }
    }

    public enum DamageType
    {
        Physical,
        Magic,
    }
}