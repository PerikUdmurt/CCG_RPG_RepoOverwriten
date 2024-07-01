using CCG.Gameplay;
using CCG.Infrastructure.Factory;
using CCG.Services;
using UnityEngine;
using Zenject;


    public class CardSlotInstaller : MonoInstaller
    {
        [SerializeField] private Transform _cardSlotEntryPoint;
        public override void InstallBindings()
        {
            Container.BindFactory<CardSlot, CustomFactory<CardSlot>>().FromFactory<CustomFactory<CardSlot>>().NonLazy();
            Container.Bind<Transform>().FromInstance(_cardSlotEntryPoint).WhenInjectedInto<ICardReciever>().NonLazy();
            Container.BindInterfacesAndSelfTo<CardReciever>().AsSingle().NonLazy();
        }
    }

