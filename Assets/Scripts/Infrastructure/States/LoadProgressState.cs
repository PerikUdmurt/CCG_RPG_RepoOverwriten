using CCG.Data;
using CCG.Services.SaveLoad;
using CCG.Services.SceneLoader;
using UnityEngine;

namespace CCG.Infrastructure.States
{
    public class LoadProgressState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private IPersistentProgressService _persistentProgressService;
        private readonly ISavedLoadService _savedLoadService;

        public LoadProgressState(GameStateMachine gameStateMachine, PersistentProgressService persistentProgressService, SavedLoadService savedLoadService)
        {
            _gameStateMachine = gameStateMachine;
            _persistentProgressService = persistentProgressService;
            _savedLoadService = savedLoadService;
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
            _persistentProgressService.playerProgress = _savedLoadService.LoadProgress() ?? NewProgress();
        }

        private PlayerProgress NewProgress()
        {
            return new PlayerProgress("Gameplay");
        }
    }
}