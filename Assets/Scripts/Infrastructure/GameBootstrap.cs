using UnityEngine;
using Zenject;

namespace CCG.Infrastructure
{
    public class GameBootstrap : MonoBehaviour, ICoroutineRunner 
    {
        private GameStateMachine _stateMachine;

        [Inject]
        public void Constuct(GameStateMachine gameStateMachine) 
        {
            _stateMachine = gameStateMachine;
        }

        private void Awake()
        {
            _stateMachine.Enter<BootstrapState>();

            DontDestroyOnLoad(this);
        }
    }
}
