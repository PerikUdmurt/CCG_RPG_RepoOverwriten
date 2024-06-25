using CCG.Gameplay;
using UnityEngine;
using Zenject;

namespace CCG.UI.Hints
{
    public class CardHintCreator : MonoBehaviour
    {
        private ICard _card;
        private HintService _hintManager;

        [Inject]
        private void Construct(HintService hintService)
        {
            _hintManager = hintService;
        }

        private void Awake()
        {
            _card = GetComponent<ICard>();
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

        private async void ShowHint()
        {
            await _hintManager.CreateHint(_card.Name, _card.CardDescription, Color.blue);
            if (_card.Effects.Count > 0)
            {
                foreach (var effect in _card.Effects)
                {
                    if (effect != null)
                        await _hintManager.CreateHint(effect.effectName, effect.description, Color.green);
                }
            }
        }

        private void HideHint()
        {
            _hintManager.DeleteHint();
        }
    }
}
