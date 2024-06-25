using System;
using UnityEngine;

namespace CCG.Gameplay.Hand
{
    public class Dragable : MonoBehaviour, IDragable
    {
        public event Action Taken;
        public event Action Dropped;

        [SerializeField] private float dragLerpValue;
        public bool isDragable { get; set; } = true;

        public void Drag(Vector3 targetPosition)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, dragLerpValue);
        }

        public void Drop()
        {
            Dropped?.Invoke();
        }

        public void Take()
        {
            Taken?.Invoke();
            gameObject.TryGetComponent<Card>(out Card card);
            card.StateMachine.Enter(CardState.isDragging);
        }
    }
}
