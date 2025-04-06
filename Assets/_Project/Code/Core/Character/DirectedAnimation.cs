using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Project.Code.Core.Character
{
    [System.Serializable]
    public class DirectedAnimation
    {
        [FormerlySerializedAs("Animation")] public AnimationClip Clip;
        public Vector2 Direction;
        public bool Flip;

        public override bool Equals(object obj)
        {
            return obj is DirectedAnimation animation &&
                   Direction == animation.Direction &&
                   Flip == animation.Flip;
        }
    }
}