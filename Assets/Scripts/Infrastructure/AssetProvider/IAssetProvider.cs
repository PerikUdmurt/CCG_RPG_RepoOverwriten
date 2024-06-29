using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CCG.Infrastructure.AssetProvider
{
    public interface IAssetProvider
    {
        void CleanUp();
        void Initialize();
        UniTask<T> Load<T>(AssetReference assetReference) where T : class;
        UniTask<T> Load<T>(string address) where T : class;
    }
}