using CCG.StaticData.Effects;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CCG.StaticData.Cards
{
    [CreateAssetMenu(fileName ="EmptyCard",menuName = "StaticData/Card")]
    public class CardStaticData: ScriptableObject
    {
        public CardType CardID;
        public string Name;
        public string CardDescription;
        public DeckType DeckType;
        public int ValueOfCard;
        public List<CardEffect> Effects;
    }
}
