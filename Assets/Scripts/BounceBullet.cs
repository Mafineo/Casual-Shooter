using System.Collections.Generic;
using UnityEngine;

namespace Game.Enitites
{
    public class BounceBullet : Bullet
    {
        private const float BounceRadius = 8f;

        private int _bounces = 3;

        public void SetBouncesCount(int bounces)
        {
            _bounces = bounces;
        }

        protected override void OnHit()
        {
            if (_bounces > 0)
            {
                Collider[] colliders = Physics.OverlapSphere(transform.position, BounceRadius);
                List<IHittable> _targets = new List<IHittable>();
                foreach (Collider collider in colliders)
                {
                    if (collider.TryGetComponent(out IHittable hittable) && _target != hittable)
                    {
                        _targets.Add(hittable);
                    }
                }
                if (_targets.Count > 0)
                {
                    BounceBullet bullet = Instantiate(this, transform.position, Quaternion.identity);
                    bullet.SetBouncesCount(--_bounces);
                    bullet.SetTarget(_targets[Random.Range(0, _targets.Count)]);
                }
            }
            base.OnHit();
        }
    }
}