using CCG.Data;
using CCG.Gameplay;
using CCG.Infrastructure.Factory;
using CCG.Infrastructure.ObjectPool;
using CCG.Services;
using CCG.Services.Input;
using CCG.Services.SaveLoad;
using CCG.Services.SceneLoader;
using System.Linq;
using UnityEngine;
using Zenject;

namespace CCG.Infrastructure.Installers
{
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
            BindInputService();
            Container.BindInterfacesAndSelfTo<SceneLoader>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<PersistentProgressService>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<SavedLoadService>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<CardStaticDataService>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<AssetProvider.AssetProvider>().AsSingle().NonLazy();
        }

        private void BindInputService()
        {
            Container.Bind<IInputService>().To<MobileInputService>().AsSingle().NonLazy();
        }

        private void BindSpawner()
        {
            Container.Bind<ISpawner>().To<GameSpawner>().AsSingle();
        }

        private void BindFactories()
        {
            Container.BindFactory<Card, CustomFactory<Card>>().FromFactory<CustomFactory<Card>>().NonLazy();
            Container.BindFactory<CardSlot, CustomFactory<CardSlot>>().FromFactory<CustomFactory<CardSlot>>().NonLazy();
        }
    }
}