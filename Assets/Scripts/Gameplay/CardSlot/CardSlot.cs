using CCG.Infrastructure;
using CCG.UI;
using System;
using UnityEngine;

namespace CCG.Gameplay
{
    public class CardSlot : MonoBehaviour, ICardSlot, ICustomPool
    {
        [SerializeField] private Transform _cardTransform;
        [SerializeField] private CardSlotPreview _preview;
        private ICard _currentCard;
        private IMovable _movable;

        public event Action<ICardSlot,ICard> Filled;
        public event Action<ICardSlot> Unfilled;

        public ICard CurrentCard
        {
            get => _currentCard;
            set => _currentCard = value;
        }
        public Transform Transform => transform;
        public CardSlotPreview Preview { get { return _preview; } }
        public IMovable Movable => _movable ?? (_movable = GetComponent<IMovable>());

        public void TakeCard(ICard card)
        {
            card.Movable.MoveToParent(_cardTransform);
            CurrentCard = card;
            CurrentCard.StateMachine.Enter(CardState.inCardSlot);
            Filled?.Invoke(this,card);
            card.Dragable.Taken += LoseCard;
            
        }

        public void LoseCard()
        {
            if (CurrentCard != null)
            {
                CurrentCard.Dragable.Taken -= LoseCard;
                CurrentCard = null;
                Unfilled?.Invoke(this);
            }
        }

        public void SwapCard(ICard newCard)
        {
            CurrentCard.Dragable.Taken -= LoseCard;
            CurrentCard.StateMachine.Enter(CardState.inStuckOfCard);
            CurrentCard = null;

            TakeCard(newCard);
        }


        private void ClearPreview()
        {
            _preview.CardPreviewAnimation(0);
        }

        public void OnCreated()
        {
            gameObject.SetActive(false);
        }

        public void OnReceipt()
        {
            gameObject.SetActive(true);
        }

        public void OnReleased()
        {
            CleanUp();
            gameObject.SetActive(false);
        }

        private void CleanUp()
        {
            if (CurrentCard != null)
            {
                CurrentCard.Dragable.Taken -= LoseCard;
                CurrentCard?.StateMachine.Enter(CardState.InCardReset);
            }
            CurrentCard = null;
            ClearPreview();
        }
    }
}
