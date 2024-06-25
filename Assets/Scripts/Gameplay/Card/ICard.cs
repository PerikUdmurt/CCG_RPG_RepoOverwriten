using CCG.Data;
using CCG.Gameplay.Hand;
using CCG.StaticData.Cards;
using CCG.StaticData.Effects;
using System.Collections.Generic;
using UnityEngine;

namespace CCG.Gameplay
{
    public interface ICard
    {
        public CardStateMachine StateMachine { get; }
        Sprite GetImage();
        void SetImage(Sprite sprite);
        void SetAvailability(bool dragableValue, bool usableValue, bool selectableValue);
        void UpdateData(CardData cardData);
        void SetStack(StackOfCard stack);
        void SetAvailability(bool value);

        GameObject gameObject { get;}
        CardType CardID { get; }
        string Name { get; }
        string CardDescription { get; }
        DeckType DeckType { get; }
        StackOfCard Stack { get; }
        int ValueOfCard { get; }
        List<CardEffect> Effects { get; }
        IDragable Dragable { get; }
        ISelectable Selectable { get; }
        IUsable Usable { get; }
        IMovable Movable { get; }
    }
}
