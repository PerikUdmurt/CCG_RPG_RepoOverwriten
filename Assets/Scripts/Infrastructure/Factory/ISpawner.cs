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
        void CreateObjectPools();
        Task<GameObject> SpawnCard(CardType cardType);
        Task<GameObject> SpawnCardSlot();
        Task<GameObject> SpawnHand();
    }
}
