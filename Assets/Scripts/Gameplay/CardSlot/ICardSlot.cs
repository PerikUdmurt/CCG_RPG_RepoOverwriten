using CCG.UI;
using System;
using UnityEngine;

namespace CCG.Gameplay
{
    public interface ICardSlot
    {
        CardSlotPreview Preview { get; }
        Transform Transform { get; }
        ICard CurrentCard { get; set; }
        IMovable Movable { get; }

        event Action<ICardSlot,ICard> Filled;
        event Action<ICardSlot> Unfilled;

        void LoseCard();
        void SwapCard(ICard newCard);
        void TakeCard(ICard card);
    }
}
