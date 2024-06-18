using CCG.Infrastructure;
using CCG.Infrastructure.AssetProvider;
using System.Collections.Generic;

namespace CCG.Gameplay
{
    public partial class CardStateMachine
    {
        private IExitableState _activeState = null;
        private Dictionary<CardState, IExitableState> _states;
        public CardState CurrentState { get; private set; }
        public CardStateMachine(ICard card, IAssetProvider assetProvider)
        {
            _states = new Dictionary<CardState, IExitableState>()
            {
                { CardState.inCardSlot, new InCardSlotState(card) },
                { CardState.isDragging, new IsDraggingState(card) },
                { CardState.inStuckOfCard, new InStuckState(card) },
                { CardState.inObjectPool, new InObjectPoolState(card)},
                { CardState.Init, new InitState(card, assetProvider)}
            };
        }
        public void Enter(CardState cardState)
        {
            _activeState?.Exit();
            ICardState state = GetState<ICardState>(cardState);
            CurrentState = cardState;
            _activeState = state;
            state.Enter();
        }

        public void Enter<TPayload>(CardState cardState, TPayload payload)
        {
            _activeState?.Exit();
            ICardStatePayloaded<TPayload> state = GetState<ICardStatePayloaded<TPayload>>(cardState);
            CurrentState = cardState;
            _activeState = state;
            state.Enter(payload);
        }

        private TState GetState<TState>(CardState cardState) where TState: class, IExitableState
        {
            return _states[cardState] as TState;
        }
    }
}
    
