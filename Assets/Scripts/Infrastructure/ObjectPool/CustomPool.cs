using CCG.Infrastructure.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CCG.Infrastructure.ObjectPool
{
    public class CustomPool<T> where T: MonoBehaviour, ICustomPool
    {
        private CustomFactory<T> _factory;
        private List<T> _objects;
        private CreateMethod currentCreateMethod;
        private AssetReference _assetReference;
        private string _assetPath;

        public CustomPool(CustomFactory<T> factory, AssetReference assetReference)
        {
            _factory = factory;
            _objects = new List<T>();
            _assetReference = assetReference;
            currentCreateMethod = CreateMethod.FromAssetReference; 
        }

        public CustomPool(CustomFactory<T> factory, string assetPath)
        {
            _factory = factory;
            _objects = new List<T>();
            _assetPath = assetPath;
            currentCreateMethod = CreateMethod.FromAssetPath;
        }

        public async Task Fill(int prepareObjects)
        {
            for (int i = 0; i < prepareObjects; i++)
            {
                await Create();
            }
        }

        public void Release()
        {
            foreach (var obj in _objects)
            {
                GameObject.Destroy(obj.gameObject);
            }
        }

        public async Task<T> Get()
        {
            var obj = _objects.FirstOrDefault(x => !x.isActiveAndEnabled);

            if (obj == null)
            {
                obj = await Create();
            }

            obj.OnReceipt();
            return obj;
        }

        private async Task<T> Create()
        {
            GameObject obj = await CreateGameObject();
            obj.TryGetComponent<T>(out T component);
            _objects.Add(component);
            component.OnCreated();
            return component;
        }

        private async Task<GameObject> CreateGameObject()
        {
            switch (currentCreateMethod)
            {
                case CreateMethod.FromAssetReference:
                    return await _factory.CreatePrefab(_assetReference);
                case CreateMethod.FromAssetPath:
                    return await _factory.CreatePrefab(_assetPath);
            }
            throw new Exception("Не обозначен метод создания объекта");
        }

        public void Release(T obj)
        {
            obj.OnReleased();
        }
    }
}
