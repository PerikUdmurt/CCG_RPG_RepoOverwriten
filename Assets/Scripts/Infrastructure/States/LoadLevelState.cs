using CCG.Data;
using CCG.Gameplay;
using CCG.Infrastructure.Factory;
using CCG.Infrastructure.ObjectPool;
using CCG.Services.PersistentProgress;
using CCG.Services.SceneLoader;
using CCG.StaticData.Cards;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CCG.Infrastructure.States
{
    public class LoadLevelState : IPayloadedState<SceneName>
    {
        private readonly SceneLoader _sceneLoader;
        private readonly GameStateMachine _gameStateMachine;
        private readonly ISpawner _gameSpawner;
        private readonly IPersistentProgressService _persistentProgressService;
        private CustomPool<Card> _cardPool;

        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, ISpawner factory, IPersistentProgressService persistentProgressService)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _gameSpawner = factory;
            _persistentProgressService = persistentProgressService;
        }
        public void Enter(SceneName sceneName)
        {
            _gameSpawner.CleanUp();
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        private void OnLoaded()
        {
            _gameSpawner.CreateObjectPools();
            _gameSpawner.SpawnHand();
            _gameSpawner.SpawnCardSlot();

            InformProgressReaders();
        }

        private void InformProgressReaders()
        {
            foreach (ISavedProgressReader progressReaders in _gameSpawner.ProgressReaders)
            {
                progressReaders.LoadProgress(_persistentProgressService.playerProgress);
            }
        }

        public void Exit()
        {

        }
    }
}
