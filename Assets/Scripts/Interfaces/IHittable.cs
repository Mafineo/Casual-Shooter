using UnityEngine;

namespace Game.Enitites
{
    public interface IHittable
    {
        Vector3 HitPoint { get; }
        void GetHit();
    }
}