using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;

namespace CCG.Gameplay
{
    public class CardSlotSeeker: MonoBehaviour
    {
        public Action<ICardSlot> SlotChanged;

        private ICard _card;
        private HashSet<ICardSlot> _slots = new HashSet<ICardSlot>();
        private ICardSlot _nearestSlot;
        
        private void Awake()
        {
            _card = GetComponent<ICard>();
            
        }

        private void OnEnable()
        {
            _card.Dragable.Dropped += CheckCardSlot;
            SlotChanged += SwitchPrevew;
        }

        private void OnDisable()
        {
            _card.Dragable.Dropped -= CheckCardSlot;
            SlotChanged -= SwitchPrevew;
        }

        private void Update()
        {
            if (!(_card.StateMachine.CurrentState == CardState.inCardSlot)) 
            { 
                _nearestSlot = FindNearestSlot(_slots);
            }
        }
        
        private void SwitchPrevew(ICardSlot newSlot)
        {
            if (_nearestSlot != null) { HidePreview(_nearestSlot); }
            if (newSlot != null) { ShowPreview(newSlot); }
        }
        
        private void ShowPreview(ICardSlot slot)
        {
             slot.Preview.ShowSetCardPreview(_card.GetImage());
        }

        private void HidePreview(ICardSlot slot)
        {
            slot.Preview.CardPreviewAnimation(0);
        }
        

        private void CheckCardSlot()
        {
            if (_slots.Count == 0) { _card.StateMachine.Enter(CardState.inStuckOfCard); return; }
            if (_nearestSlot.CurrentCard==null) { _nearestSlot.TakeCard(_card); }
            else {  _nearestSlot.SwapCard(_card);}
        }

        private ICardSlot FindNearestSlot(HashSet<ICardSlot> slots)
        {
            ICardSlot nearestSlot = null;
            float minDistance = float.MaxValue;
            for (int i = 0; i < slots.Count; i++)
            {
                float currentDistance = (this.transform.position - slots.ElementAt(i).Transform.position).magnitude;
                if (currentDistance < minDistance) { minDistance = currentDistance; nearestSlot = slots.ElementAt(i); }
            }
            if (nearestSlot != _nearestSlot) { SlotChanged?.Invoke(nearestSlot); }
            return nearestSlot;
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out ICardSlot cardSlot))
            {
                _slots.Add(cardSlot);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.TryGetComponent(out ICardSlot cardSlot))
            {
                _slots.Remove(cardSlot);
            }
        }
    }
}