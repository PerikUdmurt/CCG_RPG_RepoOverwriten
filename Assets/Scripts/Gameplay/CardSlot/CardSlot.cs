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
        private ICard _currentCard;
        
        [SerializeField] private CardSlotPreview _preview;

        public event Action<ICardSlot,ICard> Filled;
        public event Action<ICardSlot> Unfilled;

        public ICard CurrentCard
        {
            get { return _currentCard; }
            set
            {
                _currentCard = value;
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
            gameObject.SetActive(false);
        }
    }
}
