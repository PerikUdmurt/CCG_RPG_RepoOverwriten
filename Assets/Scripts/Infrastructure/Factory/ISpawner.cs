using CCG.Gameplay;
using CCG.Services.PersistentProgress;
using CCG.StaticData.Cards;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace CCG.Infrastructure.Factory
{
    public interface ISpawner
    {
        List<ISavedProgressReader> ProgressReaders { get; }
        List<ISavedProgress> ProgressWriters { get; }
        void CleanUp();
        Task CreateObjectPools();
        void DespawnCard(Card card);
        Task<Card> SpawnCard(CardType cardType);
        void DespawnCardSlot(CardSlot slot);
        Task<CardSlot> SpawnCardSlot();
        Task<HandController> SpawnHand();
    }
}
