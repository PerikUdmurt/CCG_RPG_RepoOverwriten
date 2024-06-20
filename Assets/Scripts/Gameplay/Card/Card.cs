using System;
using System.Collections.Generic;
using CCG.Data;
using CCG.Gameplay.Hand;
using CCG.Infrastructure;
using CCG.Infrastructure.AssetProvider;
using CCG.Services.SaveLoad;
using CCG.StaticData.Cards;
using CCG.StaticData.Effects;
using UnityEngine;
using Zenject;

namespace CCG.Gameplay
{
    public class Card : MonoBehaviour, ICard, ICustomPool, IDataSaver
    {
        private CardType cardID;

        public CardType CardID
        {
            get { return cardID; }
            set { cardID = value; }
        }

        public CardStateMachine StateMachine { get; private set; }
        [HideInInspector] public bool inCardSlot;
        [SerializeField] private float _moveSpeed;

        private SpriteRenderer _spriteRenderer;

        public string Name { get; set; }
        public string CardDescription { get; set; }
        public DeckType DeckType { get; set; }
        public StackOfCard Stack { get; set; }
        public int ValueOfCard { get; set; }
        public List<CardEffect> Effects { get; set; }
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

        public void LoadData(CardData cardData)
        {
            CardID = cardData.CardID;
            Name = cardData.Name;
            CardDescription = cardData.CardDescription;
            DeckType = cardData.DeckType;
            ValueOfCard = cardData.ValueOfCard;
        }

        public void SaveData(ref GameData gameData)
        {
            Debug.Log("Сохранена карта");
            CardData cardData = new CardData(this.cardID, Name, CardDescription, DeckType, ValueOfCard, Effects);
            gameData.cards.Add( cardData );
        }

        public void SetImage(Sprite sprite)
        {
            _spriteRenderer.sprite = sprite;
        }

        public Sprite GetImage()
        {
            return _spriteRenderer.sprite;
        }

        public void SetAvailability(bool dragableValue, bool usableValue, bool selectableValue)
        {
            Dragable.isDragable = dragableValue;
            Usable.isUsable = usableValue;
            Selectable.isSelectable = selectableValue;
        }

        public void OnCreated()
        {
            StateMachine.Enter(CardState.inObjectPool);
            gameObject.SetActive(false);
        }

        public void OnReceipt()
        {
            gameObject.SetActive(true);
        }

        public void OnReleased()
        {
            StateMachine.Enter(CardState.inObjectPool);
            gameObject?.SetActive(false);
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
    
