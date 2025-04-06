using System;
using UnityEngine;

namespace _Project.Code.Core.Collision
{
    public interface ICollisionDetector
    {
        event Action<Collider2D, CollisionType> Triggered;
        
        event Action<Collider2D> TriggerEnter;
        event Action<Collider2D> TriggerStay;
        event Action<Collider2D> TriggerExit;
    }
}