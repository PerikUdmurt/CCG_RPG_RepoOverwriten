using CCG.Gameplay;
using CCG.StaticData.Cards;
using UnityEngine;

namespace CCG.Data
{
    public static class DataExtension
    {
        public static Vector3Data AsVectorData(this Vector3 vector)
        {
            return new Vector3Data(vector.x, vector.y, vector.z);
        }

        public static Vector3 AsUnityVector(this Vector3Data vector3Data) =>
            new Vector3(vector3Data.X, vector3Data.Y, vector3Data.Z);


        public static T ToDeserialized<T>(this string json) =>
            JsonUtility.FromJson<T>(json);

        public static string ToJson(this object obj) => JsonUtility.ToJson(obj);

        public static CardData ToCardData(this CardStaticData cardStaticData) => new CardData
        {
            CardID = cardStaticData.CardID,
            Name = cardStaticData.Name,
            CardDescription = cardStaticData.CardDescription,
            ValueOfCard = cardStaticData.ValueOfCard,
            DeckType = cardStaticData.DeckType,
            Effects = cardStaticData.Effects
        };

    }
}

