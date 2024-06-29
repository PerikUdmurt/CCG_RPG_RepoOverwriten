using CCG.Infrastructure;
using CCG.Infrastructure.States;
using CCG.Services.SceneLoader;
using System.Reflection.Emit;
using UnityEngine;
using Zenject;

public class LoadSceneTrigger : MonoBehaviour
{    
    private GameStateMachine _gameStateMachine;

    [Inject]
    public void Construct(GameStateMachine gameStateMachine)
    {
        _gameStateMachine = gameStateMachine;
    }

    public void LoadScene()
    {
       _gameStateMachine.Enter<LoadLevelState, SceneName>(SceneName.Gameplay);
    }
}
