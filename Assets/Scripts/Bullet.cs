using UnityEngine;

namespace Game.Enitites
{
    public class Bullet : MonoBehaviour
    {
        private const float BulletSpeed = 15f;
        private const float HitDistance = 0.001f;

        [SerializeField] private Sprite _icon;

        protected IHittable _target;

        public Sprite Icon => _icon;

        public void SetTarget(IHittable target)
        {
            _target = target;
        }

        protected virtual void OnHit()
        {
            _target.GetHit();
            Destroy(gameObject);
        }

        private void Update()
        {
            transform.position = Vector3.MoveTowards(transform.position, _target.Position, Time.deltaTime * BulletSpeed);
            Vector3 targetVector = _target.Position - transform.position;
            if (targetVector != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(targetVector);
            }
            if (Vector3.Distance(transform.position, _target.Position) <= HitDistance)
            {
                OnHit();
            }
        }
    }
}