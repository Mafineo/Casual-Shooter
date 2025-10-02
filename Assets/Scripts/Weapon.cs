using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Enitites
{
    public abstract class Weapon : MonoBehaviour
    {
        private enum WeaponState { Idle, PreparingForNextShot }

        [Header("Parameters")]
        [SerializeField] private List<Bullet> _availableBullets;
        [Header("Settings")]
        [SerializeField] private GameObject _globalTarget;
        [SerializeField] protected Transform _muzzle;

        private Bullet _currentBullet;
        private WeaponState _state;

        public Bullet[] BulletList => _availableBullets.ToArray();

        public virtual bool LoadBullet(int bulletId)
        {
            if (bulletId >= _availableBullets.Count) return false;
            _currentBullet = _availableBullets[bulletId];
            _state = WeaponState.PreparingForNextShot;
            PrepareForNextShot();
            return true;
        }    

        public virtual bool Shoot(IHittable target)
        {
            if (target == null || _state != WeaponState.Idle) return false;
            Bullet bullet = Instantiate(_currentBullet, _muzzle.position, Quaternion.identity);
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
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (_globalTarget.TryGetComponent(out IHittable hittable))
                {
                    Shoot(hittable);
                }
            }
        }
    }
}