using UnityEngine;

namespace Game.Enitites
{
    public class Turtle : Enemy
    {
        private static readonly int GetHitHash = Animator.StringToHash("GetHit");

        [Header("Settings")]
        [SerializeField] private Animator _animator;
        [SerializeField] private float _height;

        public override Vector3 Position => transform.position + Vector3.up * _height;

        public override void GetHit()
        {
            _animator.Play(GetHitHash);
        }
    }
}