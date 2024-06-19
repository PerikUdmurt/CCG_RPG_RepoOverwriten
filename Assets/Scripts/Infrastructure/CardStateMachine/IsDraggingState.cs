using System;

namespace CCG.Gameplay
{

    public partial class CardStateMachine
    {
        public class IsDraggingState : ICardState
        {
            private ICard card;

            public IsDraggingState(ICard card)
            {
                this.card = card;
            }

            public void Enter()
            {
                
            }

            public void Exit()
            {
                
            }
        }
    }
}
    
