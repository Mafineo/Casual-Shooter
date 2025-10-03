using UnityEngine;

namespace Game.Enitites.Enemies
{
    public class Turtle : MonoBehaviour, IHittable
    {
        private static readonly int Get_Hit_Hash = Animator.StringToHash("GetHit");

        [Header("Settings")]
        [SerializeField] private Animator animator;
        [SerializeField] private Transform hitPoint;

        public Vector3 HitPoint => hitPoint.position;

        public void GetHit()
        {
            animator.Play(Get_Hit_Hash);
        }
    }
}