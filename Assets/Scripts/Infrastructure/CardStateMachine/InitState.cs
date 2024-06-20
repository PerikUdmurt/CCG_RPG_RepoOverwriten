using CCG.Data;
using CCG.Infrastructure.AssetProvider;
using System.Threading.Tasks;
using UnityEngine;

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
        }

        private async Task SetImage(CardData cardData)
        {
            Sprite sprite = await assetProvider.Load<Sprite>(cardData.CardID.ToString());
            card.SetImage(sprite);
        }

        private void PlayInitAnimation()
        {
            //Сделать анимацию доставания из колоды в отдельном классе CardAnimation. Вызвать отсюда
        }
    }
}