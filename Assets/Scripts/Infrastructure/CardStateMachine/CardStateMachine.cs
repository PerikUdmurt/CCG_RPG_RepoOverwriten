using CCG.Infrastructure;
using CCG.Infrastructure.AssetProvider;
using CCG.Services.Stack;
using System.Collections.Generic;
using UnityEngine;

namespace CCG.Gameplay
{
    public partial class CardStateMachine
    {
        private IExitableState _activeState = null;
        private Dictionary<CardState, IExitableState> _states;
        public CardState CurrentState { get; private set; }
        public CardStateMachine(ICard card, IAssetProvider assetProvider, IStackService stackService)
        {
            _states = new Dictionary<CardState, IExitableState>()
            {
                { CardState.inCardSlot, new InCardSlotState(card) },
                { CardState.isDragging, new IsDraggingState(card) },
                { CardState.inStuckOfCard, new InStuckState(card, stackService) },
                { CardState.inObjectPool, new InObjectPoolState(card)},
                { CardState.Init, new InitState(card, assetProvider, stackService)},
                { CardState.InCardReset, new InCardResetState(card)}
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
    
