using System.Collections.Generic;
using UnityEngine;

namespace _Project.Code.Core.Character
{
    [CreateAssetMenu(fileName = "AnimationConfig", menuName = "Configs/AnimationConfig")]
    public class TopDownAnimationConfig : ScriptableObject
    {
        public List<DirectedAnimation> Animations = new List<DirectedAnimation>()
        {
            new DirectedAnimation()
            {
                Clip = null,
                Direction = Vector2.down
            },

            new DirectedAnimation()
            {
                Clip = null,
                Direction = Vector2.left
            },
            new DirectedAnimation()
            {
                Clip = null,
                Direction = Vector2.up
            },
            new DirectedAnimation()
            {
                Clip = null,
                Direction = Vector2.right
            },
            new DirectedAnimation()
            {
                Clip = null,
                Direction = Vector2.down + Vector2.left
            },
            new DirectedAnimation()
            {
                Clip = null,
                Direction = Vector2.up + Vector2.left
            },
            new DirectedAnimation()
            {
                Clip = null,
                Direction = Vector2.up + Vector2.right
            },
            new DirectedAnimation()
            {
                Clip = null,
                Direction = Vector2.down + Vector2.right
            }
        };
    }
}