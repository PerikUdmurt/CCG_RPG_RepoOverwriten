using CCG.Infrastructure.AssetProvider;
using CCG.Infrastructure.States;
using CCG.Services;
using CCG.Services.SceneLoader;
using UnityEngine;

namespace CCG.Infrastructure
{
    public class BootstrapState : IState
    {
        private const string LoadSceneName = "Gameplay";
        private const string InitialSceneName = "Initial";

        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly ICardStaticDataService _cardStaticDataService;
        private readonly IAssetProvider _assetProvider;

        public BootstrapState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, ICardStaticDataService moduleStaticDataService, IAssetProvider assetProvider)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _cardStaticDataService = moduleStaticDataService;
            _assetProvider = assetProvider;
        }

        public void Enter()
        {
            _sceneLoader.Load(InitialSceneName, EnterLoadLevel);
            _cardStaticDataService.LoadModules();
            _assetProvider.Initialize();
        }

        private void EnterLoadLevel()
        {
            _gameStateMachine.Enter<LoadProgressState>();
        }

        public void Exit()
        {
            
        }
    }
}
