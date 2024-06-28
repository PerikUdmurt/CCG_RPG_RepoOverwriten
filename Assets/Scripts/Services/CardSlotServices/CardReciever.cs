using CCG.Gameplay;
using CCG.Infrastructure.Factory;
using CCG.StaticData.Cards;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace CCG.Services
{
    public class CardReciever : ICardReciever
    {
        private readonly ISpawner _spawner;
        private readonly Transform _cardSlotEntryPos;
        private HashSet<ICardSlot> _cardSlots = new HashSet<ICardSlot>();
        private Dictionary<ICardSlot, ICard> _filledSlots = new Dictionary<ICardSlot, ICard>();

        public event Action Updated;

        public CardReciever(ISpawner spawner, Transform cardSlotEntryPos) 
        {
            _spawner = spawner;
            _cardSlotEntryPos = cardSlotEntryPos;
        }



        public async Task CreateCardSlot()
        {
            CardSlot cardSlot = await _spawner.SpawnCardSlot();
            cardSlot.transform.position = _cardSlotEntryPos.position + new Vector3(0.65f * _cardSlots.Count,0f,0f);
            _cardSlots.Add(cardSlot);
            cardSlot.Filled += AddToDictionary;
            cardSlot.Unfilled += RemoveFromDictionary;
        }

        public void DeleteCardSlot()
        {

        }

        public void CleanUp()
        {

        }

        public List<ICard> GetCombination()
        {
            List<ICard> combination = new List<ICard>();
            foreach(var card in _filledSlots.Values)
            {
                combination.Add(card);
            }
            return combination;
        }

        private void AddToDictionary(ICardSlot cardSlot, ICard card)
        {
            _filledSlots.Add(cardSlot, card);
            Updated?.Invoke();
        }

        private void RemoveFromDictionary(ICardSlot cardSlot)
        {
            _filledSlots.Remove(cardSlot);
            Updated?.Invoke();
        }

        public void SetSettings()
        {
            throw new NotImplementedException();
        }
    }

    public interface ICardReciever
    {
        event Action Updated;
        public void SetSettings();
        Task CreateCardSlot();
        void DeleteCardSlot();
        List<ICard> GetCombination();
    }

    public class CardSlotInstaller : MonoInstaller
    {
        [SerializeField] private Transform _cardSlotEntryPoint;
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<CardReciever>().AsSingle().NonLazy();
            Container.BindInstance<Transform>(_cardSlotEntryPoint).WhenInjectedInto<ICardReciever>();
        }
    }
}
