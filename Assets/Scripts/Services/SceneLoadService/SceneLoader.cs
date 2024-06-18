using CCG.Infrastructure;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CCG.Services.SceneLoader
{
    public class SceneLoader
    {
        private readonly ICoroutineRunner _coroutineRunner;
        public SceneLoader(ICoroutineRunner runner)
        {
            _coroutineRunner = runner;
        }

        public void Load(string sceneName, Action onLoaded = null)
        {
            Debug.Log(sceneName);
            Debug.Log(_coroutineRunner.GetType().Name);
            _coroutineRunner.StartCoroutine(LoadScene(sceneName, onLoaded));
        }

        private IEnumerator LoadScene(string sceneName, Action onLoaded = null)
        {
            if (SceneManager.GetActiveScene().name == sceneName)
            {
                onLoaded?.Invoke();
                yield break;
            }

            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);

            while (!asyncOperation.isDone)
            {
                yield return null;
            }

            onLoaded?.Invoke();
        }
    }
}
