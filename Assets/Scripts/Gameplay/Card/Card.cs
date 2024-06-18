using System;
using System.Collections.Generic;
using CCG.Gameplay.Hand;
using CCG.Infrastructure;
using CCG.Infrastructure.AssetProvider;
using CCG.StaticData.Cards;
using CCG.StaticData.Effects;
using UnityEngine;
using Zenject;

namespace CCG.Gameplay
{
    [Serializable]
    public class Card : MonoBehaviour, ICard, ICustomPool
    {
        [SerializeField] private CardType cardID; 

        public CardType CardID 
        { 
            get {  return cardID; }
            set { cardID = value; }
        }
        public string Name { get; set; }
        public string CardDescription { get; set; }
        public DeckType DeckType { get; set; }
        public int ValueOfCard { get; set; }
        public List<CardEffect> Effects { get; set; }

        public CardStateMachine StateMachine { get; private set; }
        [HideInInspector] public bool inCardSlot;
        [SerializeField] private float _moveSpeed;

        private SpriteRenderer _spriteRenderer;

        public IDragable Dragable { get; set; }
        public ISelectable Selectable { get; set; }
        public IUsable Usable { get; set; }
        public IMovable Movable { get; set; }
        GameObject ICard.gameObject => gameObject;

        [Inject]
        public void Construct(IAssetProvider assetProvider)
        {
            RegisterComponents();
            StateMachine = new CardStateMachine(this, assetProvider);
        }

        public void SetImage(Sprite sprite)
        {
            _spriteRenderer.sprite = sprite;
        }

        public Sprite GetImage()
        {
            return _spriteRenderer.sprite;
        }

        public void OnCreated()
        {
            gameObject.SetActive(false);
            Debug.Log("OnCreated");
        }

        public void OnReceipt()
        {
            gameObject.SetActive(true);
            Debug.Log("OnPeceipt");
        }

        public void OnReleased()
        {
            gameObject?.SetActive(false);
            Debug.Log("OnReleased");
        }

        private void RegisterComponents()
        {
            Usable = GetComponent<IUsable>();
            Selectable = GetComponent<ISelectable>();
            Dragable = GetComponent<IDragable>();
            Movable = GetComponent<IMovable>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }
    }
}
    
