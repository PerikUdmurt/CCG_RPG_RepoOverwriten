using CCG.Data;
using CCG.Gameplay;
using CCG.Infrastructure.Factory;
using CCG.Infrastructure.ObjectPool;
using CCG.Services.Input;
using CCG.Services.PersistentProgress;
using CCG.Services.SceneLoader;
using CCG.StaticData.Cards;
using CCG.UI;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace CCG.Infrastructure.States
{
    public class LoadLevelState : IPayloadedState<string>
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
        public void Enter(string sceneName)
        {
            _gameSpawner.CleanUp();
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        private void OnLoaded()
        {
            _gameSpawner.CreateObjectPools();
            _gameSpawner.SpawnHand();
            _gameSpawner.SpawnCard(CardType.Strenght);
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
