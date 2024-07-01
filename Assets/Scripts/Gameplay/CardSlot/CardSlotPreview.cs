using CCG.Gameplay;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

namespace CCG.UI
{
    public class CardSlotPreview : MonoBehaviour
    {
        public float alpha;
        public float duration;

        [SerializeField] private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            if (_spriteRenderer == null) 
            {_spriteRenderer = GetComponent<SpriteRenderer>();}
        }
        public void ShowSetCardPreview(Sprite sprite)
        {
            _spriteRenderer.color.WithAlpha(0);
            _spriteRenderer.sprite = sprite;
            CardPreviewAnimation(alpha);
        }

        public void ShowSwapCardPreview(Sprite sprite)
        {
            //Сделать стрелочки по кругу, которые показывают, что мы меняем карты
            _spriteRenderer.sprite = sprite;
            CardPreviewAnimation(alpha);
        }

        public void CardPreviewAnimation(float alpha)
        {
            _spriteRenderer.DOFade(alpha, duration);
        }
    }
}
