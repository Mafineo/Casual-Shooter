using UnityEngine;

namespace Game.Enitites
{
    public abstract class Enemy : MonoBehaviour, IHittable
    {
        public abstract Vector3 Position { get; }

        public abstract void GetHit();
    }
}