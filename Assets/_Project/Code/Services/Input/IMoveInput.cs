using UnityEngine;

namespace _Project.Code.Services.Input
{
    public interface IMoveInput
    {
        Vector2 Direction { get; }
    }
}