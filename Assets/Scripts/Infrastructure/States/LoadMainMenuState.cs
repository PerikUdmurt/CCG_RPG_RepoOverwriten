using CCG.Infrastructure.Factory;
using CCG.Services.SaveLoad;
using CCG.Services.SceneLoader;
using System;

namespace CCG.Infrastructure.States
{
    public class LoadMainMenuState : IState, IDataLoader
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IDataPersistentService _dataPersistentService;
        private readonly SceneLoader _sceneLoader;

        public LoadMainMenuState(GameStateMachine gameStateMachine, IDataPersistentService dataPersistentService, SceneLoader sceneLoader)
        {
            _gameStateMachine = gameStateMachine;
            _dataPersistentService = dataPersistentService;
            _sceneLoader = sceneLoader;
        }

        public void Enter()
        {
            _sceneLoader.Load(SceneName.Mainmenu, OnLoaded);
            CreateHUD();
            LoadData(_dataPersistentService.GameData);
            //Снять шторку
        }

        private void OnLoaded()
        {
        
        }

        public void Exit()
        {

        }

        public void LoadData(GameData gamedata)
        {
            //Описать какие настройки будут загружаться
        }

        private void CreateHUD()
        {

        }
    }
}
