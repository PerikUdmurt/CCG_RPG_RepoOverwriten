using CCG.Infrastructure.Factory;
using CCG.Infrastructure.States;
using CCG.Services;
using CCG.Services.SaveLoad;
using CCG.Services.SceneLoader;
using CCG.StaticData.Cards;
using System;
using System.Reflection;
using UnityEngine;
using Zenject;

namespace CCG.Infrastructure.SceneHelper
{
    public class GamePlaySceneHelper : MonoBehaviour
    {
        private ISpawner gameSpawner;
        private IDataPersistentService dataPersistentService;
        private ICardReciever _cardReciever;

        [Inject]
        public void Construct(ISpawner spawner, IDataPersistentService dataPersistentService, ICardReciever cardReciever)
        {
            gameSpawner = spawner;
            _cardReciever = cardReciever;
            this.dataPersistentService = dataPersistentService;
        }

        public void SpawnCard(CardType cardType)
        {
            gameSpawner.SpawnCardByStaticData(cardType, new Vector3());
            Debug.Log("SpawnedCardByHelper. CardType: " +  cardType);
        }

        public void SaveProgress()
        {
            dataPersistentService.SaveGame();
        }

        public void LoadProgress()
        {
            dataPersistentService.LoadGame();
        }

        public void NewProgress() 
        { 
            dataPersistentService.NewGame();
        }

        public void StartCardReciever(int max, int prep)
        {
            _cardReciever.Start(max, prep);
        }

        public void StopCardReciever()
        {
            _cardReciever.CleanUp();
        }
    }
}
