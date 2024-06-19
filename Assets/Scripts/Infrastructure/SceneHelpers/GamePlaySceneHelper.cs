using CCG.Infrastructure.Factory;
using CCG.Services.SceneLoader;
using CCG.StaticData.Cards;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace CCG.Infrastructure.SceneHelper
{
    public class GamePlaySceneHelper : MonoBehaviour
    {
        private ISpawner gameSpawner;
        private SceneLoader sceneLoader;

        [Inject]
        public void Construct(ISpawner spawner, SceneLoader sceneLoader)
        {
            gameSpawner = spawner;
            this.sceneLoader = sceneLoader;
        }

        public void SpawnCard(CardType cardType)
        {
            gameSpawner.SpawnCard(cardType);
            Debug.Log("SpawnedCardByHelper. CardType: " +  cardType);
        }

        public void SpawnCardSlot()
        {
            gameSpawner.SpawnCardSlot();
        }

        public void LoadScene(SceneName sceneName)
        {
            sceneLoader.Load(sceneName);
        }

    }
}
