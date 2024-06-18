namespace CCG.Gameplay
{
    public partial class CardStateMachine
    {
        public class InCardSlotState : ICardState
        {
            private ICard card;

            public InCardSlotState(ICard card)
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
    
