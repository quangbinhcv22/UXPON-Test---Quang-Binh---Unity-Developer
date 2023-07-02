using UnityEngine;

namespace Entities
{
    public class Bullet : MonoBehaviour
    {
        public int damage = 1;
        public AudioClip impactSfx;
        public AudioSource audioSource;

        protected void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Character target))
            {
                target.TakeDamage(damage, DamageType.Magic);
                audioSource.PlayOneShot(impactSfx);
            }
        }
    }
}