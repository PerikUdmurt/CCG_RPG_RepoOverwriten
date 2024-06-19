﻿using CCG.Infrastructure.AssetProvider;
using CCG.StaticData.Cards;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CCG.Gameplay
{
    internal class InitState : ICardStatePayloaded<InitCardPayload>
    {
        private ICard card;
        private readonly IAssetProvider assetProvider;

        public InitState(ICard card, IAssetProvider assetProvider)
        {
            this.card = card;
            this.assetProvider = assetProvider;
        }

        public async void Enter(InitCardPayload payload)
        {
            card.SetAvailability(false,false,false);
            UpdateInfo(payload.CardStaticData);
            await SetImage(payload);
            PlayInitAnimation();
            card.StateMachine.Enter(CardState.inStuckOfCard, card.Stack);
        }

        public void Exit()
        {
            
        }

        private void UpdateInfo(CardStaticData cardStaticData)
        {
            card.CardID = cardStaticData.CardID;
            card.Name = cardStaticData.Name;
            card.CardDescription = cardStaticData.CardDescription;
            card.DeckType = cardStaticData.DeckType;
            card.ValueOfCard = cardStaticData.ValueOfCard;
            Debug.Log("Updated");
        }

        private async Task SetImage(InitCardPayload payload)
        {
            AssetReference reference = payload.CardStaticData.CardSprite;
            Sprite sprite = await assetProvider.Load<Sprite>(reference);
            card.SetImage(sprite);
            Debug.Log("SetImage");
        }

        private void PlayInitAnimation()
        {
            //Сделать анимацию доставания из колоды
        }
    }

    public struct InitCardPayload
    {
        public CardStaticData CardStaticData;

        public InitCardPayload(CardStaticData staticData)
        {
            CardStaticData = staticData;
        }
    }
}