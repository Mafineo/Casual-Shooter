using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using Game.Enitites.Bullets;

namespace Game.Enitites.Weapons
{
    public abstract class Weapon : MonoBehaviour
    {
        private enum WeaponState { Idle, PreparingForNextShot }

        [Header("Parameters")]
        [SerializeField] private List<Bullet> availableBullets;
        [Header("Settings")]
        [SerializeField] private GameObject globalTarget;
        [SerializeField] protected Transform muzzle;

        private Bullet _currentBullet;
        private WeaponState _state;

        public Bullet[] BulletList => availableBullets.ToArray();

        public virtual bool LoadBullet(int bulletId)
        {
            if (bulletId >= availableBullets.Count) return false;
            _currentBullet = availableBullets[bulletId];
            _state = WeaponState.PreparingForNextShot;
            PrepareForNextShot();
            return true;
        }    

        public virtual bool Shoot(IHittable target)
        {
            if (target == null || _state != WeaponState.Idle) return false;
            Bullet bullet = Instantiate(_currentBullet, muzzle.position, Quaternion.identity);
            bullet.SetTarget(target);
            _state = WeaponState.PreparingForNextShot;
            PrepareForNextShot();
            return true;
        }

        protected virtual UniTask PrepareForNextShot()
        {
            _state = WeaponState.Idle;
            return UniTask.CompletedTask;
        }

        private void Update()
        {
            CheckInput();
        }

        private void CheckInput()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (globalTarget.TryGetComponent(out IHittable hittable))
                {
                    Shoot(hittable);
                }
            }
        }
    }
}