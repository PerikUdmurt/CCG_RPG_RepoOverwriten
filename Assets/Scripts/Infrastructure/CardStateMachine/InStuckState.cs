using CCG.Services.Stack;

namespace CCG.Gameplay
{

    public partial class CardStateMachine
    {
        public class InStuckState : ICardState
        {
            private ICard card;
            private readonly IStackService _stackService;

            public InStuckState(ICard card, IStackService stackService)
            {
                this.card = card;
                _stackService = stackService;
            }

            public void Enter()
            {
                card.SetAvailability(false, false, false);
                StackOfCard stackOfCard = _stackService.GetStack(card.DeckType);
                stackOfCard.AddToStack(card);
                card.Movable.MoveTo(stackOfCard.CardTransform.position);
                card.SetAvailability(true, true, true);
            }

            public void Exit()
            {
                card.Stack.RemoveFromStack(card);
            }
        }
    }
}
    
