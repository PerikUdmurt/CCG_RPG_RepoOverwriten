using CCG.Gameplay;
using CCG.Infrastructure.AssetProvider;
using CCG.Infrastructure.Factory;
using CCG.Infrastructure.ObjectPool;
using CCG.StaticData.Cards;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace CCG.Services
{
    public class CardReciever : ICardReciever
    {
        private readonly Transform _cardSlotEntryPos;
        private int _maxCount;
        private Vector3 _spawnPos;
        private List<ICardSlot> _cardSlots = new List<ICardSlot>();
        private Dictionary<ICardSlot, ICard> _filledSlots = new Dictionary<ICardSlot, ICard>();
        private CustomPool<CardSlot> _pool;

        public event Action Updated;

        public CardReciever(Transform cardSlotEntryPos, CustomFactory<CardSlot> factory) 
        {
            _cardSlotEntryPos = cardSlotEntryPos;
            _pool = new CustomPool<CardSlot>(factory, AssetPath.CardSlot);
        }

        public async UniTask Start(int maxCount, int prepareSlot) => 
            await Start(maxCount, prepareSlot, _cardSlotEntryPos.position);

        public async UniTask Start(int maxCount, int prepareSlot, Vector3 atPosition)
        {
            CleanUp();
            _maxCount = maxCount;
            _spawnPos = atPosition;
            for (int a = 0; a < prepareSlot; a++)
            {
                ICardSlot cardSlot = await CreateCardSlot(atPosition);
            }
        }

        private void DeleteCardSlot(CardSlot cardSlot)
        {
            _pool.Release(cardSlot);
        }

        private async UniTask<ICardSlot> CreateCardSlot(Vector3 atPosition)
        {
            if (_cardSlots.Count >= _maxCount) return null;
            CardSlot cardSlot = await _pool.Get();
            cardSlot.transform.position = atPosition + new Vector3(0.65f * _cardSlots.Count,0f,0f);
            _cardSlots.Add(cardSlot);
            cardSlot.Filled += AddToDictionary;
            cardSlot.Unfilled += RemoveFromDictionary;
            return cardSlot;
        }


        public void CleanUp()
        {
            foreach (CardSlot cardSlot in _cardSlots)
            {
                DeleteCardSlot(cardSlot);
            }
            _cardSlots.Clear();
            _filledSlots.Clear();
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
            if (_filledSlots.ContainsKey(cardSlot))
            {
                _filledSlots[cardSlot] = card;
            }
            else
            {
                _filledSlots.Add(cardSlot, card);
            }
            Updated?.Invoke();
            Sort();
        }

        private void RemoveFromDictionary(ICardSlot cardSlot)
        {
            _filledSlots.Remove(cardSlot);
            _cardSlots.Remove(cardSlot);
            Updated?.Invoke();
            Sort();
        }

        private void Sort()
        {
            int last = 0;
            for(int first = 0;  first < _cardSlots.Count; first++)
            {
                if (_filledSlots.ContainsKey(_cardSlots[first]))
                {
                    _filledSlots[_cardSlots[first]].Movable.MoveTo(_spawnPos + new Vector3(0.65f * first, 0f, 0f));
                }
                else
                {
                    _cardSlots[first].Movable.MoveTo(_spawnPos + new Vector3(0.65f * (_cardSlots.Count - last), 0f, 0f));
                    last++;
                }
            }

            if (_filledSlots.Count >= _cardSlots.Count && _filledSlots.Count < _maxCount) 
            {
                CreateCardSlot(_spawnPos);
            }
        }
    }
}
