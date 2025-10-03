using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game.Enitites.Weapons
{
    public class Pistol : Weapon
    {
        private const float Reload_Time = 1f;
        private static readonly int Shoot_Hash = Animator.StringToHash("Shoot");

        [Header("Settings")]
        [SerializeField] private Animator animator;
        [SerializeField] private GameObject shootEffect;

        protected override async UniTask PrepareForNextShot()
        {
            await UniTask.WaitForSeconds(Reload_Time);
            await base.PrepareForNextShot();
        }

        public override bool Shoot(IHittable target)
        {
            if (base.Shoot(target))
            {
                animator.SetTrigger(Shoot_Hash);
                Instantiate(shootEffect, muzzle.transform.position, Quaternion.identity);
                return true;
            }
            return false;
        }
    }
}