using UnityEngine;

namespace Game.Enitites
{
    public class ExplosiveBullet : Bullet
    {
        private const float ExplosionRadius = 8f; 

        [Header("Settings")]
        [SerializeField] private GameObject _explosionEffect;

        protected override void OnHit()
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, ExplosionRadius);
            foreach (Collider collider in colliders)
            {
                if (collider.TryGetComponent(out IHittable hittable))
                {
                    hittable.GetHit();
                }
            }
            Instantiate(_explosionEffect, transform.position, Quaternion.identity);
            base.OnHit();
        }
    }
}