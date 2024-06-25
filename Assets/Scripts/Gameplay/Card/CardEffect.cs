using UnityEngine;

namespace CCG.StaticData.Effects
{
    [CreateAssetMenu(fileName = "EmptyEffect", menuName = "CardEffect")]
    public class CardEffect : ScriptableObject
    {
        public string effectName;
        public string description;
    }
}
