using CCG.Gameplay.Hand;
using CCG.Infrastructure;
using CCG.UI;
using System;
using UnityEngine;
using Zenject;

namespace CCG.Gameplay
{
    public class CardSlot : MonoBehaviour, ICardSlot, ICustomPool
    {
        public event Action Changed;

        private ICard _currentCard;
        
        [SerializeField] private CardSlotPreview _preview;
        public ICard CurrentCard
        {
            get { return _currentCard; }
            set
            {
                _currentCard = value;
                Changed?.Invoke();
            }
        }

        public Transform Transform => transform;

        public CardSlotPreview Preview { get { return _preview; } }

        public void SetDefaultState()
        {
            LoseCard();
        }

        public void TakeCard(ICard card)
        {
            card.Movable.MoveTo(this.transform.position);
            CurrentCard = card;
            CurrentCard.StateMachine.Enter(CardState.inCardSlot);
            card.Dragable.Taken += LoseCard;
        }

        public void LoseCard()
        {
            if (CurrentCard != null)
            {
                CurrentCard.Dragable.Taken -= LoseCard;
                CurrentCard = null;
            }
        }

        public void SwapCard(ICard newCard)
        {
            CurrentCard.Dragable.Taken -= LoseCard;
            CurrentCard.StateMachine.Enter(CardState.inStuckOfCard);
            CurrentCard = null;

            TakeCard(newCard);
        }

        public void OnCreated()
        {

        }

        public void OnReceipt()
        {
           
        }

        public void OnReleased()
        {
           
        }
    }

    public interface ICardSlot
    {
        CardSlotPreview Preview { get; }
        Transform Transform { get; }
        ICard CurrentCard { get; set; }

        event Action Changed;

        void LoseCard();
        void SwapCard(ICard newCard);
        void TakeCard(ICard card);
    }
}
