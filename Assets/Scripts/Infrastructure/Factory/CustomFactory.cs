using System.Threading.Tasks;
using CCG.Infrastructure.AssetProvider;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace CCG.Infrastructure.Factory
{
    public class CustomFactory<T>: PlaceholderFactory<T> where T : MonoBehaviour
    {
        private DiContainer _container;
        private IAssetProvider _assetProvider;

        public CustomFactory(DiContainer container, IAssetProvider assetProvider)
        {
            _container = container;
            _assetProvider = assetProvider;
        }

        public async Task<GameObject> CreatePrefab(string assetPath)
        {
            GameObject prefab = await _assetProvider.Load<GameObject>(assetPath);
            GameObject result = _container.InstantiatePrefab(prefab);
            return result;
        }

        public async Task<GameObject> CreatePrefab(AssetReference assetReference)
        {
            GameObject prefab = await _assetProvider.Load<GameObject>(assetReference);
            GameObject result = _container.InstantiatePrefab(prefab);
            return result;
        }
    }
}
    
