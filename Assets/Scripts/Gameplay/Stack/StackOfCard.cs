using System;
using System.Collections.Generic;
using UnityEngine;
using CCG.Gameplay.Hand;

namespace CCG.Gameplay
{
    public class StackOfCard : MonoBehaviour, ISelectable, IUsable, IStackOfCard
    {
        public event Action Used;
        public event Action Selected;
        public event Action Deselected;

        [SerializeField] private Transform _cardTransform;

        public bool isUsable { get; set; } = true;
        public bool isSelectable { get; set; } = true;

        public Transform CardTransform => _cardTransform;

        private Dictionary<ICard, int> _activeCards = new Dictionary<ICard, int>();

        public void Use()
        {
            ShowCard();
            Used?.Invoke();
        }

        public void AddToStack(ICard card)
        {
            _activeCards.Add(card, card.ValueOfCard);
            Sort();
        }

        public void RemoveFromStack(ICard card) 
        {  
            _activeCards.Remove(card);
            Sort();
        }

        private void Sort()
        {

        }

        private void ShowCard()
        {
            foreach (ICard card in _activeCards.Keys)
            {
                card.SetAvailability(true,true,true);
            }
        }

        private void HideCard()
        {
            foreach (ICard card in _activeCards.Keys)
            {
                card.SetAvailability(false, false, false);
            }
        }

        public void Select()
        {
            Selected?.Invoke();
        }

        public void Deselect()
        {
            Deselected?.Invoke();
        }
    }

    public interface IStackOfCard
    {
        Transform CardTransform { get; }
        void AddToStack(ICard card);
        void RemoveFromStack(ICard card);
    }
}