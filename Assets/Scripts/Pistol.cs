using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game.Enitites
{
    public class Pistol : Weapon
    {
        private const float ReloadTime = 1f;
        private static readonly int ShootHash = Animator.StringToHash("Shoot");

        [Header("Settings")]
        [SerializeField] private Animator _animator;
        [SerializeField] private GameObject _shootEffect;

        protected override async UniTask PrepareForNextShot()
        {
            await UniTask.WaitForSeconds(ReloadTime);
            await base.PrepareForNextShot();
        }

        public override bool Shoot(IHittable target)
        {
            if (base.Shoot(target))
            {
                _animator.SetTrigger(ShootHash);
                Instantiate(_shootEffect, _muzzle.transform.position, Quaternion.identity);
                return true;
            }
            return false;
        }
    }
}