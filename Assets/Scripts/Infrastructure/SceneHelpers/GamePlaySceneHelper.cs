using CCG.Infrastructure.Factory;
using CCG.Services.SaveLoad;
using CCG.Services.SceneLoader;
using CCG.StaticData.Cards;
using UnityEngine;
using Zenject;

namespace CCG.Infrastructure.SceneHelper
{
    public class GamePlaySceneHelper : MonoBehaviour
    {
        private ISpawner gameSpawner;
        private SceneLoader sceneLoader;
        private IDataPersistentService dataPersistentService;

        [Inject]
        public void Construct(ISpawner spawner, SceneLoader sceneLoader, IDataPersistentService dataPersistentService)
        {
            gameSpawner = spawner;
            this.sceneLoader = sceneLoader;
            this.dataPersistentService = dataPersistentService;
        }

        public void SpawnCard(CardType cardType)
        {
            gameSpawner.SpawnCardByStaticData(cardType);
            Debug.Log("SpawnedCardByHelper. CardType: " +  cardType);
        }

        public void SpawnCardSlot()
        {
            gameSpawner.SpawnCardSlot();
        }

        public void LoadScene(SceneName sceneName)
        {
            sceneLoader.Load(sceneName);
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
    }
}
