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
        void LoadData(CardData cardData);

        GameObject gameObject { get;}

        IDragable Dragable {  get; set; }
        ISelectable Selectable { get; set; }
        IUsable Usable { get; set; }
        
        IMovable Movable { get; set; }
        CardType CardID { get; set; }
        string Name { get; set; }
        string CardDescription { get; set; }
        DeckType DeckType { get; set; }
        int ValueOfCard { get; set; }
        List<CardEffect> Effects { get; set; }
        StackOfCard Stack { get; set; }
    }
}
