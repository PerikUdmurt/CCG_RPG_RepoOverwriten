using CCG.Data;
using CCG.Gameplay;
using CCG.Infrastructure.Factory;
using CCG.Services.SaveLoad;
using CCG.Services.SceneLoader;

namespace CCG.Infrastructure.States
{
    public class LoadLevelState : IPayloadedState<SceneName>, IDataLoader
    {
        private readonly SceneLoader _sceneLoader;
        private readonly GameStateMachine _gameStateMachine;
        private readonly ISpawner _gameSpawner;
        private readonly IDataPersistentService _dataPersistentService;

        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, ISpawner factory, IDataPersistentService dataPersistentService)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _gameSpawner = factory;
            _dataPersistentService = dataPersistentService;
        }
        public void Enter(SceneName sceneName)
        {
            _gameSpawner.CleanUp();
            _sceneLoader.Load(sceneName, OnLoaded);
        }
        public void LoadData(GameData gameData)
        {
            SpawnCardsByLoadData(gameData);
        }

        private void OnLoaded()
        {
            _gameSpawner.CreateObjectPools();
            _gameSpawner.SpawnHand();

            LoadData(_dataPersistentService.GameData);
        }

        private void SpawnCardsByLoadData(GameData gameData)
        {
            foreach (CardData cardData in gameData.cards)
            {
                _gameSpawner.SpawnCard(cardData);
            }
        }

        public void Exit()
        {

        }
    }
}
