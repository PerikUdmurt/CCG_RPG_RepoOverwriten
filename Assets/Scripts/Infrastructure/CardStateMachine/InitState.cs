using CCG.Infrastructure.AssetProvider;
using CCG.StaticData.Cards;
using CCG.StaticData.Effects;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CCG.Gameplay
{
    internal class InitState : ICardStatePayloaded<CardData>
    {
        private ICard card;
        private readonly IAssetProvider assetProvider;

        public InitState(ICard card, IAssetProvider assetProvider)
        {
            this.card = card;
            this.assetProvider = assetProvider;
        }

        public async void Enter(CardData payload)
        {
            card.SetAvailability(false,false,false);
            UpdateInfo(payload);
            await SetImage(payload);
            PlayInitAnimation();
            //card.StateMachine.Enter(CardState.inStuckOfCard, card.Stack);
        }

        public void Exit()
        {
            
        }

        private void UpdateInfo(CardData cardData)
        {
            card.LoadData(cardData);
            Debug.Log("Updated");
        }

        private async Task SetImage(CardData cardData)
        {
            Sprite sprite = await assetProvider.Load<Sprite>(cardData.CardID.ToString());
            card.SetImage(sprite);
            Debug.Log("SetImage");
        }

        private void PlayInitAnimation()
        {
            //Сделать анимацию доставания из колоды в отдельном классе CardAnimation. Вызвать отсюда
        }
    }

    public struct CardData
    {
        public CardType CardID;
        public string Name;
        public string CardDescription;
        public DeckType DeckType;
        public int ValueOfCard;
        public List<CardEffect> Effects;

        public CardData(CardType cardID, string name, string cardDescription, DeckType deckType, int valueOfCard, List<CardEffect> effects)
        {
            CardID = cardID;
            Name = name;
            CardDescription = cardDescription;
            DeckType = deckType;
            ValueOfCard = valueOfCard;
            Effects = effects;
        }
    }
}