using CCG.Data;
using CCG.Gameplay;
using CCG.Services.SaveLoad;
using CCG.StaticData.Cards;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace CCG.Infrastructure.Factory
{
    public interface ISpawner
    {
        List<IDataSaver> DataSavers { get; }
        void CleanUp();
        Task CreateObjectPools();
        void DespawnCard(Card card);
        void DespawnCardSlot(CardSlot slot);
        Task<CardSlot> SpawnCardSlot();
        Task<HandController> SpawnHand();
        void ReleaseObjectPools();
        Task<HUD> SpawnHUD();
        Task<Card> SpawnCardByStaticData(CardType cardType, Vector3 atPosition);
        Task<Card> SpawnCard(CardData cardData, Vector3 atPosition);
    }
}
