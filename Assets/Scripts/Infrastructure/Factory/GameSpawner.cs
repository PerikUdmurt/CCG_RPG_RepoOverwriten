using System.Collections.Generic;
using UnityEngine;
using CCG.Services;
using CCG.Infrastructure.AssetProvider;
using System.Threading.Tasks;
using CCG.Gameplay;
using CCG.StaticData.Cards;
using CCG.Infrastructure.ObjectPool;
using CCG.Data;
using CCG.Services.SaveLoad;

namespace CCG.Infrastructure.Factory
{
    public class GameSpawner : ISpawner
    {
        public List<IDataSaver> DataSavers { get; } = new List<IDataSaver>();

        private readonly ICardStaticDataService _cardStaticDataService;
        private readonly IAssetProvider _assetProvider;
        private readonly CustomFactory<Card> _cardFactory;
        private readonly CustomFactory<CardSlot> _cardslotFactory;

        private CustomPool<Card> _cardPool = null;
        private CustomPool<CardSlot> _cardSlotPool = null;

        public GameSpawner(ICardStaticDataService moduleStaticDataService, IAssetProvider assetProvider, CustomFactory<Card> customFactory, CustomFactory<CardSlot> cardslotFactory)
        {
            _cardStaticDataService = moduleStaticDataService;
            _assetProvider = assetProvider;
            _cardFactory = customFactory;
            _cardslotFactory = cardslotFactory;
        }

        public void CleanUp()
        {
            DataSavers.Clear();
            ReleaseObjectPools();

            _assetProvider.CleanUp();
        }
        public async Task<HandController> SpawnHand()
        {
            GameObject resource = await _assetProvider.Load<GameObject>(AssetPath.Hand);
            GameObject gameObj = GameObject.Instantiate(resource);
            gameObj.TryGetComponent<HandController>(out HandController handController);
            return handController;
        }

        public async Task<Card> SpawnCardByStaticData(CardType cardType)
        {
            CardStaticData staticData = _cardStaticDataService.GetStaticData(cardType);
            CardData data = staticData.ToCardData();
            return await SpawnCard(data);
        }

        public async Task<Card> SpawnCard(CardData cardData)
        {
            Card card = await _cardPool.Get();
            card.StateMachine.Enter(CardState.Init, cardData);
            Register(card);
            return card;
        }

        public void DespawnCard(Card card)
        {
            Unregister(card);
            _cardPool.Release(card);
        }

        public async Task<CardSlot> SpawnCardSlot()
        {
            return await _cardSlotPool.Get();
        }

        public void DespawnCardSlot(CardSlot cardSlot)
        {
            _cardSlotPool.Release(cardSlot);
        }

        public async Task CreateObjectPools()
        {
            _cardPool = new CustomPool<Card>(_cardFactory, AssetPath.Card);
            await _cardPool.Fill(30);
            _cardSlotPool = new CustomPool<CardSlot>(_cardslotFactory, AssetPath.CardSlot);
            await _cardSlotPool.Fill(3);
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
