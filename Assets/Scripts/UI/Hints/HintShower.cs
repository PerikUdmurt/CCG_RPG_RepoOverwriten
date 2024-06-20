using CCG.Gameplay.Hand;
using UnityEngine;
using Zenject;

namespace CCG.UI.Hints
{
    public class HintShower : MonoBehaviour
    {
        private ISelectable _selectableObj;
        private IHintService _hintManager;

        [SerializeField] private string _name;
        [SerializeField] private string _hintText;
        [SerializeField] private Color _hintColor;

        [Inject]
        public void Construct(IHintService hintManager)
        {
            _hintManager = hintManager;
        }

        private void Awake()
        {
            _selectableObj = GetComponent<ISelectable>();
        }

        private void OnEnable()
        {
            _selectableObj.Selected += ShowHint;
            _selectableObj.Deselected += HideHint;
        }

        private void OnDisable()
        {
            _selectableObj.Selected -= ShowHint;
            _selectableObj.Deselected -= HideHint;
        }

        private void ShowHint()
        {
            _hintManager.CreateHint(_name, _hintText, Color.blue);
        }

        private void HideHint()
        {
            _hintManager.DeleteHint();
        }
    }
}
