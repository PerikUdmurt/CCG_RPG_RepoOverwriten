using System;
using System.Collections.Generic;
using CCG.Data;
using CCG.Infrastructure.AssetProvider;
using CCG.Infrastructure.Factory;
using CCG.Infrastructure.States;
using CCG.Services;
using CCG.Services.Input;
using CCG.Services.SaveLoad;
using CCG.Services.SceneLoader;
using UnityEngine;

namespace CCG.Infrastructure
{
    public class GameStateMachine
    {
        private readonly Dictionary<Type, IExitableState> _states;
        private IExitableState _activeState = null;

        public GameStateMachine(SceneLoader sceneLoader, PersistentProgressService persistentProgressService, SavedLoadService savedLoadService, ISpawner gameFactory, ICardStaticDataService cardStaticDataService, IAssetProvider assetProvider)
        {
            _states = new Dictionary<Type, IExitableState>()
            {
                [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader, cardStaticDataService, assetProvider),
                [typeof(LoadLevelState)] = new LoadLevelState(this, sceneLoader, gameFactory, persistentProgressService),
                [typeof(LoadProgressState)] = new LoadProgressState(this,persistentProgressService, savedLoadService)
            };
        }

        public void Enter<TState>() where TState : class, IState
        {
            _activeState?.Exit();
            IState state = GetState<TState>();
            _activeState = state;
            state.Enter();
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState :class, IPayloadedState<TPayload>
        {
            _activeState?.Exit();
            IPayloadedState<TPayload> state = GetState<TState>();
            _activeState = state;
            state.Enter(payload);
        }

        private TState GetState<TState>() where TState: class, IExitableState
        {
            return _states[typeof(TState)] as TState;
        }
    }
}
