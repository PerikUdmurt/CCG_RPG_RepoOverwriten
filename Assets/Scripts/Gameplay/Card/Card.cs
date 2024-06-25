using System.Collections.Generic;
using CCG.Data;
using CCG.Gameplay.Hand;
using CCG.Infrastructure;
using CCG.Infrastructure.AssetProvider;
using CCG.Services.SaveLoad;
using CCG.Services.Stack;
using CCG.StaticData.Cards;
using CCG.StaticData.Effects;
using UnityEngine;
using Zenject;

namespace CCG.Gameplay
{
    [RequireComponent(typeof(Usable))]
    [RequireComponent(typeof(Selectable))]
    [RequireComponent(typeof(Dragable))]
    [RequireComponent(typeof(Movable))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class Card : MonoBehaviour, ICard, ICustomPool, IDataSaver
    {
        private CardType _cardID;
        private string _cardName;
        private string _description;
        private DeckType _deckType;
        [SerializeField] private StackOfCard _stackOfCard;
        private int _valueOfCard;
        private List<CardEffect> _effects = new List<CardEffect>();
        private IDragable _dragable;
        private ISelectable _selectable;
        private IMovable _movable;
        private IUsable _usable;
        private SpriteRenderer _spriteRenderer;

        public CardStateMachine StateMachine { get; private set; }
        [HideInInspector] public bool inCardSlot;
        [SerializeField] private float _moveSpeed;

        public CardType CardID { get => _cardID;}
        public string Name { get => _cardName; }
        public string CardDescription { get => _description; }
        public DeckType DeckType { get => _deckType;}
        public StackOfCard Stack { get => _stackOfCard; }
        public int ValueOfCard { get => _valueOfCard; }
        public List<CardEffect> Effects { get => _effects; }
        public IDragable Dragable   { get => _dragable ?? GetComponent<IDragable>();}
        public ISelectable Selectable { get => _selectable ?? GetComponent<ISelectable>(); }
        public IUsable Usable { get => _usable ?? GetComponent<IUsable>(); }
        public IMovable Movable { get => _movable ?? GetComponent<IMovable>(); }
        public SpriteRenderer SpriteRenderer { get => _spriteRenderer ?? GetComponent<SpriteRenderer>(); }
        GameObject ICard.gameObject => gameObject;

        [Inject]
        public void Construct(IAssetProvider assetProvider, IStackService stackService)
        {
            RegisterComponents();
            StateMachine = new CardStateMachine(this, assetProvider, stackService);
        }

        public void UpdateData(CardData cardData)
        {
            _cardID = cardData.CardID;
            _cardName = cardData.Name;
            _description = cardData.CardDescription;
            _deckType = cardData.DeckType;
            _valueOfCard = cardData.ValueOfCard;
        }

        public void SaveData(ref GameData gameData)
        {
            CardData cardData = new CardData(this._cardID, Name, CardDescription, DeckType, ValueOfCard, Effects);
            gameData.cards.Add( cardData );
        }

        public void SetImage(Sprite sprite)
        {
            _spriteRenderer.sprite = sprite;
        }

        public void SetStack(StackOfCard stack)
        {
            _stackOfCard = stack;
        }

        public Sprite GetImage()
        {
            return _spriteRenderer.sprite;
        }

        public void SetAvailability(bool value)
        {
            SetAvailability(value, value, value);
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
            _usable = GetComponent<IUsable>();
            _selectable = GetComponent<ISelectable>();
            _dragable = GetComponent<IDragable>();
            _movable = GetComponent<IMovable>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }
    }
}
    
