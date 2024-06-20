using CCG.Gameplay;
using CCG.Infrastructure;
using CCG.Infrastructure.AssetProvider;
using CCG.Infrastructure.Factory;
using CCG.Services;
using CCG.Services.SaveLoad;
using CCG.Services.SceneLoader;
using System.Linq;
using UnityEngine;
using Zenject;


    public class BootstrapInstaller : MonoInstaller
    {
        private GameBootstrap GameBootstrap;
        public override void InstallBindings()
        {
            BindBootstraper();
            BindGameStateMachine();
            BindServices();

            BindFactories();
            BindSpawner();
        Debug.Log("aaaaaa");
        }

        private void BindBootstraper()
        {
            GameBootstrap = GameObject.FindGameObjectsWithTag("Bootstraper").First().GetComponent<GameBootstrap>();
            Container.BindInterfacesAndSelfTo<GameBootstrap>().FromInstance(GameBootstrap).AsSingle();
        }

        private void BindGameStateMachine()
        {
            Container.Bind<GameStateMachine>().AsSingle().NonLazy();
        }

        private void BindServices()
        {
            Container.BindInterfacesAndSelfTo<SceneLoader>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<CardStaticDataService>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<AssetProvider>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<DataPersistenceService>().AsSingle().NonLazy();
        }

        private void BindSpawner()
        {
            Container.BindInterfacesAndSelfTo<GameSpawner>().AsSingle().NonLazy();
        }

        private void BindFactories()
        {
            Container.BindFactory<Card, CustomFactory<Card>>().FromFactory<CustomFactory<Card>>().NonLazy();
            Container.BindFactory<CardSlot, CustomFactory<CardSlot>>().FromFactory<CustomFactory<CardSlot>>().NonLazy();
        }
    }
