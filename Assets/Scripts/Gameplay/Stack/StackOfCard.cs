using CCG.Infrastructure;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace CCG.Gameplay
{
    public class StackOfCard : MonoBehaviour, IStackOfCard
    {
        private Dictionary<ICard, int> _activeCards = new Dictionary<ICard, int>();
        
        [SerializeField] private Transform _cardTransform;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        public bool isUsable { get; set; } = true;
        public bool isSelectable { get; set; } = true;


        public Transform CardTransform => _cardTransform;
        public SpriteRenderer SpriteRenderer { get => _spriteRenderer ?? (_spriteRenderer = GetComponent<SpriteRenderer>()); }

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

        public void SetImage(Sprite sprite)
        {
            SpriteRenderer.sprite = sprite;
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
    }
}