using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CCG.Infrastructure.AssetProvider
{
    public interface IAssetProvider
    {
        void CleanUp();
        void Initialize();
        Task<T> Load<T>(AssetReference assetReference) where T : class;
        Task<T> Load<T>(string address) where T : class;
    }
}