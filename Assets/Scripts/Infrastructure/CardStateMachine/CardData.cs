using CCG.StaticData.Cards;
using CCG.StaticData.Effects;
using System;
using System.Collections.Generic;

namespace CCG.Data
{
    [Serializable]
    public struct CardData
    {
        public CardType CardID;
        public string Name;
        public string CardDescription;
        public DeckType DeckType;
        public int ValueOfCard;
        public List<CardEffect> Effects;

        public CardData(CardType cardID, string name, string cardDescription, DeckType deckType, int valueOfCard, List<CardEffect> effects)
        {
            CardID = cardID;
            Name = name;
            CardDescription = cardDescription;
            DeckType = deckType;
            ValueOfCard = valueOfCard;
            Effects = effects;
        }
    }
}