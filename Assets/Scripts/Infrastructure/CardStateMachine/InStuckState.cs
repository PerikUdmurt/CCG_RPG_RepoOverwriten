namespace CCG.Gameplay
{

    public partial class CardStateMachine
    {
        public class InStuckState : ICardStatePayloaded<IStackOfCard>
        {
            private ICard card;

            public InStuckState(ICard card)
            {
                this.card = card;
            }

            public void Enter(IStackOfCard stackOfCard)
            {
                card.SetAvailability(false, false, false);
                card.Movable.MoveTo(stackOfCard.CardTransform.position);
                card.SetAvailability(true, true, true);
            }

            public void Exit()
            {
                
            }
        }
    }
}
    
