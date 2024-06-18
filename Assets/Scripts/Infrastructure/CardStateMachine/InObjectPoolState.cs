namespace CCG.Gameplay
{

    public partial class CardStateMachine
    {
        public class InObjectPoolState : ICardState
        {
            private ICard card;

            public InObjectPoolState(ICard card)
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
    
