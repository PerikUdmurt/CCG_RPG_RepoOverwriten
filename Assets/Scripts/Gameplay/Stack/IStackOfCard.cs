using UnityEngine;

namespace CCG.Gameplay
{
    public interface IStackOfCard
    {
        Transform CardTransform { get; }
        SpriteRenderer SpriteRenderer { get; }

        void AddToStack(ICard card);
        void RemoveFromStack(ICard card);
        void SetImage(Sprite sprite);
    }
}