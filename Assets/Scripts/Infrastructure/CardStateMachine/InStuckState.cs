namespace CCG.Gameplay
{

    public partial class CardStateMachine
    {
        public class InStuckState : ICardState
        {
            private ICard card;

            public InStuckState(ICard card)
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
    
