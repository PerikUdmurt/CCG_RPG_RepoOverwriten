using CCG.Gameplay;
using UnityEngine;
using Zenject;

namespace CCG.UI.Hints
{
    public class CardHintCreator : MonoBehaviour
    {
        private ICard _card;
        private HintManager _hintManager;

        [Inject]
        private void Construct(HintManager hintManager)
        {
            _hintManager = hintManager;
        }

        private void Awake()
        {
            _card = GetComponent<Card>();
        }

        private void OnEnable()
        {
            _card.Selectable.Selected += ShowHint;
            _card.Selectable.Deselected += HideHint;
        }

        private void OnDisable()
        {
            _card.Selectable.Selected -= ShowHint;
            _card.Selectable.Deselected -= HideHint;
        }

        private void ShowHint()
        {
            _hintManager.CreateHint(_card.Name, _card.CardDescription, Color.blue);
            if (_card.Effects.Count > 0)
            {
                foreach (var effect in _card.Effects)
                {
                    if (effect != null)
                        _hintManager.CreateHint(effect.effectName, effect.description, Color.green);
                }
            }
        }

        private void HideHint()
        {
            _hintManager.DeleteHint();
        }
    }
}
