using CCG.Data;
using CCG.Services.SaveLoad;
using CCG.Services.SceneLoader;
using UnityEngine;

namespace CCG.Infrastructure.States
{
    public class LoadProgressState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IDataPersistentService _dataPersistentService;

        public LoadProgressState(GameStateMachine gameStateMachine, IDataPersistentService dataPersistentService)
        {
            _gameStateMachine = gameStateMachine;
            _dataPersistentService = dataPersistentService;
        }

        public void Enter()
        {
            LoadProgressOrInitNew();
            
            _gameStateMachine.Enter<LoadLevelState,SceneName>(SceneName.Gameplay);
        }

        public void Exit()
        {
            
        }

        private void LoadProgressOrInitNew()
        {
            _dataPersistentService.LoadGame();
        }
    }
}