using UnityEngine;

namespace Game.Enitites.Bullets
{
    public class ExplosiveBullet : Bullet
    {
        private const float Explosion_Radius = 8f; 

        [Header("Settings")]
        [SerializeField] private GameObject _explosionEffect;

        protected override void OnHit()
        {
            IHittable[] hittables = FindHittablesInRange(transform.position, Explosion_Radius);
            foreach (IHittable hittable in hittables)
            {
                hittable.GetHit();
            }
            Instantiate(_explosionEffect, transform.position, Quaternion.identity);
            base.OnHit();
        }
    }
}