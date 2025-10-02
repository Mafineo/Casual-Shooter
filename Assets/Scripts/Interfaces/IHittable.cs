using UnityEngine;

namespace Game.Enitites
{
    public interface IHittable
    {
        Vector3 Position { get; }
        void GetHit();
    }
}