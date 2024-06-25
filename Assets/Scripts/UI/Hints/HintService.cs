using CCG.Infrastructure.AssetProvider;
using CCG.Infrastructure.Factory;
using CCG.Infrastructure.ObjectPool;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace CCG.UI.Hints
{
    public class HintService: IHintService
    {
        private RectTransform _hintEntryPos;
        private List<HintUI> _hintList = new List<HintUI>();
        private CustomPool<HintUI> _hintPool;

        private RectTransform _currentHintsEntryPoint;

        public HintService(CustomFactory<HintUI> hintFactory) 
        { 
            _currentHintsEntryPoint = _hintEntryPos;
            _hintPool = new CustomPool<HintUI>(hintFactory, AssetPath.Hint);
        }

        public async Task CreateObjectPool()
        {
            await _hintPool.Fill(3);
        }

        public async Task CreateHint(string name, string hintText, Color color) 
        {
            HintUI currentHint = await _hintPool.Get();
            
            currentHint.transform.SetParent(_currentHintsEntryPoint.transform, false);
            _currentHintsEntryPoint = currentHint.rectTransform;

            currentHint.hintName.text = name;
            currentHint.description.text = hintText;

            _hintList.Add(currentHint);
        }

        public void DeleteHint()
        { 
            foreach (var hint in _hintList)
            {
                _hintPool.Release(hint);
            }
            _hintList.Clear();
            _currentHintsEntryPoint = _hintEntryPos;
        }

        public void SetHintEntryPos(RectTransform hintEntryPos)
        {
            _hintEntryPos = hintEntryPos;
        }
    }
}
