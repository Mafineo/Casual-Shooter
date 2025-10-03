using UnityEngine;
using System.Linq;
using System.Collections.Generic;

namespace Game.Enitites.Bullets
{
    public class Bullet : MonoBehaviour
    {
        private const float Bullet_Speed = 15f;
        private const float Hit_Distance = 0.001f;
        private const int Hittable_Search_Buffer_Size = 16;

        [SerializeField] private Sprite icon;

        protected IHittable target;

        public Sprite Icon => icon;

        protected static IHittable[] FindHittablesInRange(Vector3 position, float radius)
        {
            Collider[] colliders = new Collider[Hittable_Search_Buffer_Size];
            int collidersCount = Physics.OverlapSphereNonAlloc(position, radius, colliders);
            List<IHittable> targets = new List<IHittable>();
            for (int i = 0; i < collidersCount; i++)
            {
                if (colliders[i].TryGetComponent(out IHittable hittable))
                {
                    targets.Add(hittable);
                }
            }
            return targets.ToArray();
        }

        protected static IHittable GetRandomHittable(IHittable[] hittables, IHittable exception = null)
        {
            if (exception != null)
            {
                hittables = hittables.Where(h => h != exception).ToArray();
            }
            if (hittables != null && hittables.Length > 0)
            {
                return hittables[Random.Range(0, hittables.Length)];
            }
            else
            {
                return null;
            }
        }

        public void SetTarget(IHittable target) 
        {
            if (target == null)
            {
                Destroy(this);
                return;
            }
            this.target = target;
        }

        protected virtual void OnHit()
        {
            target.GetHit();
            OnDespawn();
        }

        protected virtual void OnDespawn()
        {
            Destroy(gameObject);
        }

        protected virtual void MoveToTarget()
        {
            if (target == null)
            {
                OnDespawn();
                return;
            }
            Vector3 hitPoint = target.HitPoint;
            transform.position = Vector3.MoveTowards(transform.position, hitPoint, Time.deltaTime * Bullet_Speed);
            Vector3 targetVector = hitPoint - transform.position;
            if (targetVector != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(targetVector);
            }
            if (Vector3.Distance(transform.position, hitPoint) <= Hit_Distance)
            {
                OnHit();
            }
        }

        private void Update()
        {
            MoveToTarget();
        }
    }
}