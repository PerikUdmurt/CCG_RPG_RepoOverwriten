using CCG.Data;
using CCG.Gameplay;
using CCG.Services.SaveLoad;
using CCG.StaticData.Cards;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CCG.Infrastructure.Factory
{
    public interface ISpawner
    {
        List<IDataSaver> DataSavers { get; }
        void CleanUp();
        UniTask CreateObjectPools();
        void DespawnCard(Card card);
        void DespawnCardSlot(CardSlot slot);
        UniTask<CardSlot> SpawnCardSlot();
        UniTask<HandController> SpawnHand();
        void ReleaseObjectPools();
        UniTask<HUD> SpawnHUD();
        UniTask<Card> SpawnCardByStaticData(CardType cardType, Vector3 atPosition);
        UniTask<Card> SpawnCard(CardData cardData, Vector3 atPosition);
    }
}
