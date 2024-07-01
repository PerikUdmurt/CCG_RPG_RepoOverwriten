using CCG.Data;
using CCG.Gameplay;
using CCG.Infrastructure.Factory;
using CCG.Services.SaveLoad;
using CCG.Services.SceneLoader;
using CCG.Services.Stack;
using CCG.StaticData.Cards;
using CCG.UI.Hints;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace CCG.Infrastructure.States
{
    public class LoadLevelState : IPayloadedState<SceneName>, IDataLoader
    {
        private readonly SceneLoader _sceneLoader;
        private readonly GameStateMachine _gameStateMachine;
        private readonly ISpawner _gameSpawner;
        private readonly IDataPersistentService _dataPersistentService;
        private readonly IHintService _hintService;
        private readonly IStackService _stackService;

        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, ISpawner factory, IDataPersistentService dataPersistentService, IHintService hintService, IStackService stackService)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _gameSpawner = factory;
            _dataPersistentService = dataPersistentService;
            _hintService = hintService;
            _stackService = stackService;
        }
        public void Enter(SceneName sceneName)
        {
            _gameSpawner.CleanUp();
            _stackService.CleanUp();
            _sceneLoader.Load(sceneName, OnLoaded);
        }
        public void LoadData(GameData gameData)
        {
            SpawnCardsByLoadData(gameData, new Vector3());
        }

        private async void OnLoaded()
        {
            SpawnStacks();
            await _gameSpawner.CreateObjectPools();
            await _gameSpawner.SpawnHand();
            await _hintService.CreateObjectPool();
            await SpawnHUD();

            LoadData(_dataPersistentService.GameData);
        }

        private void SpawnCardsByLoadData(GameData gameData, Vector3 atPosition)
        {
            foreach (CardData cardData in gameData.availableCards)
            {
                _gameSpawner.SpawnCard(cardData, atPosition);
            }
        }

        private async Task SpawnHUD()
        {
            HUD hud = await _gameSpawner.SpawnHUD();
            _hintService.SetHintEntryPos(hud.HintEntryPos);
        }

        private void SpawnStacks()
        {
            _stackService.SetStacksEntryPos(GameObject.FindGameObjectWithTag("StacksEntryPoint").transform.position);
            _stackService.CreateStack(DeckType.Health);
            _stackService.CreateStack(DeckType.Strenght);
            _stackService.CreateStack(DeckType.Agility);
            _stackService.CreateStack(DeckType.Intellect);
            _stackService.CreateStack(DeckType.Charisma);
            _stackService.CreateStack(DeckType.Item);
        }

        public void Exit()
        {

        }
    }
}
