using Zenject;
using CCG.Services.PersistentProgress;
using CCG.UI;
using System.Collections.Generic;
using UnityEngine;
using CCG.Services;
using CCG.Infrastructure.AssetProvider;
using System.Threading.Tasks;
using CCG.Gameplay;
using CCG.StaticData.Cards;
using CCG.Infrastructure.ObjectPool;

namespace CCG.Infrastructure.Factory
{
    public class GameSpawner : ISpawner
    {
        public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
        public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();

        private readonly ICardStaticDataService _cardStaticDataService;
        private readonly IAssetProvider _assetProvider;
        private readonly CustomFactory<Card> _cardFactory;
        private readonly CustomFactory<CardSlot> _cardslotFactory;

        private CustomPool<Card> _cardPool;
        private CustomPool<CardSlot> _cardSlotPool;

        public GameSpawner(ICardStaticDataService moduleStaticDataService, IAssetProvider assetProvider, CustomFactory<Card> customFactory, CustomFactory<CardSlot> cardslotFactory)
        {
            _cardStaticDataService = moduleStaticDataService;
            _assetProvider = assetProvider;
            _cardFactory = customFactory;
            _cardslotFactory = cardslotFactory;
        }

        public void CleanUp()
        {
            ProgressReaders.Clear();
            ProgressWriters.Clear();

            _assetProvider.CleanUp();
        }
        public async Task<HandController> SpawnHand()
        {
            GameObject resource = await _assetProvider.Load<GameObject>(AssetPath.Hand);
            GameObject gameObj = GameObject.Instantiate(resource);
            gameObj.TryGetComponent<HandController>(out HandController handController);
            return handController;
        }

        public async Task<Card> SpawnCard(CardType cardType)
        {
            Card card = await _cardPool.Get();
            CardStaticData data = _cardStaticDataService.GetStaticData(cardType);
            InitCardPayload initCardPayload = new InitCardPayload(data);
            card.StateMachine.Enter(CardState.Init, initCardPayload);
            return card;
        }

        public void DespawnCard(Card card)
        {
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

        private void Register(ISavedProgressReader progressReader)
        {
            if (progressReader is ISavedProgress progress)
            ProgressWriters.Add(progressReader);

            ProgressReaders.Add(progressReader);
        }

        private void RegisterProgress(GameObject createdObject, Vector3 atPosition)
        {
            foreach (ISavedProgressReader progressReader in createdObject.GetComponentsInChildren<ISavedProgressReader>())
            {
                Register(progressReader);
            }

        }

        private void RegisterProgress(GameObject createdObject)
        {
            foreach (ISavedProgressReader progressReader in createdObject.GetComponentsInChildren<ISavedProgressReader>())
            {
                Register(progressReader);
            }
        }
    }
}
