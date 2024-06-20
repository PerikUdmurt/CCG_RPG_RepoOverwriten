using CCG.Infrastructure.AssetProvider;
using CCG.Infrastructure.Factory;
using CCG.Infrastructure.ObjectPool;
using System.Collections.Generic;
using UnityEngine;

namespace CCG.UI.Hints
{
    public class HintService: IHintService
    {
        private RectTransform _hintEntryPos;
        private List<HintUI> _hintList = new List<HintUI>();
        private CustomPool<HintUI> _hintPool;

        private RectTransform _currentHintsEntryPoint;

        public HintService(RectTransform hintsEntryPos, CustomFactory<HintUI> hintFactory) 
        { 
            _hintEntryPos = hintsEntryPos;
            _currentHintsEntryPoint = _hintEntryPos;
            _hintPool = new CustomPool<HintUI>(hintFactory, AssetPath.Hint);
        }

        public void CreateHint(string name, string hintText, Color color) 
        {
            HintUI currentHint = _hintPool.Get().Result;
            
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
    }
}
