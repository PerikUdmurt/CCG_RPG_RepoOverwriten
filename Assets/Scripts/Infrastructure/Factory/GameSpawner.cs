using System.Collections.Generic;
using UnityEngine;
using CCG.Services;
using CCG.Infrastructure.AssetProvider;
using CCG.Gameplay;
using CCG.StaticData.Cards;
using CCG.Infrastructure.ObjectPool;
using CCG.Data;
using CCG.Services.SaveLoad;
using Cysharp.Threading.Tasks;

namespace CCG.Infrastructure.Factory
{
    public class GameSpawner : ISpawner
    {
        private readonly ICardStaticDataService _cardStaticDataService;
        private readonly IAssetProvider _assetProvider;
        private readonly CustomFactory<Card> _cardFactory;

        private CustomPool<Card> _cardPool = null;
        private CustomPool<CardSlot> _cardSlotPool = null;
        public List<IDataSaver> DataSavers { get; } = new List<IDataSaver>();

        public GameSpawner(ICardStaticDataService moduleStaticDataService, IAssetProvider assetProvider, CustomFactory<Card> customFactory)
        {
            _cardStaticDataService = moduleStaticDataService;
            _assetProvider = assetProvider;
            _cardFactory = customFactory;
        }

        public void CleanUp()
        {
            DataSavers.Clear();
            ReleaseObjectPools();

            _assetProvider.CleanUp();
        }
        public async UniTask<HandController> SpawnHand()
        {
            GameObject resource = await _assetProvider.Load<GameObject>(AssetPath.Hand);
            GameObject gameObj = GameObject.Instantiate(resource);
            gameObj.TryGetComponent<HandController>(out HandController handController);
            return handController;
        }

        public async UniTask<Card> SpawnCardByStaticData(CardType cardType, Vector3 atPosition)
        {
            CardStaticData staticData = _cardStaticDataService.GetStaticData(cardType);
            CardData data = staticData.ToCardData();
            return await SpawnCard(data, atPosition);
        }

        public async UniTask<HUD> SpawnHUD()
        {
            GameObject resource = await _assetProvider.Load<GameObject>(AssetPath.HUD);
            GameObject gameObj = GameObject.Instantiate(resource);
            gameObj.TryGetComponent<HUD>(out HUD hud);
            return hud;
        }

        public async UniTask<Card> SpawnCard(CardData cardData, Vector3 atPosition)
        {
            Card card = await _cardPool.Get();
            card.transform.position = atPosition;
            card.StateMachine.Enter(CardState.Init, cardData);
            Register(card);
            return card;
        }

        public void DespawnCard(Card card)
        {
            Unregister(card);
            _cardPool.Release(card);
        }

        public async UniTask CreateObjectPools()
        {
            _cardPool = new CustomPool<Card>(_cardFactory, AssetPath.Card);
            await _cardPool.Fill(30);
        }

        public void ReleaseObjectPools()
        {
            _cardPool?.Release();
            _cardSlotPool?.Release();
        }

        private void Register(IDataSaver dataSaver)
        {
            DataSavers.Add(dataSaver);
        }

        private void Unregister(IDataSaver dataSaver)
        {
            if (DataSavers.Contains(dataSaver))
            {
                DataSavers.Remove(dataSaver);
            }
        }

        private void RegisterDataSavers(GameObject createdObject)
        {
            foreach (IDataSaver progressReader in createdObject.GetComponentsInChildren<IDataSaver>())
            {
                Register(progressReader);
            }
        }

        private void UnregisterDataSavers(GameObject gameObject)
        {
            foreach (IDataSaver progressReader in gameObject.GetComponentsInChildren<IDataSaver>())
            {
                Unregister(progressReader);
            }
        }
    }
}
