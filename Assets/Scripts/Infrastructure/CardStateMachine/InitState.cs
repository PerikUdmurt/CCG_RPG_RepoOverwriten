using CCG.Data;
using CCG.Infrastructure.AssetProvider;
using CCG.Services.Stack;
using System.Threading.Tasks;
using UnityEngine;

namespace CCG.Gameplay
{
    internal class InitState : ICardStatePayloaded<CardData>
    {
        private ICard card;
        private readonly IAssetProvider assetProvider;
        private readonly IStackService _stackService;

        public InitState(ICard card, IAssetProvider assetProvider, IStackService stackService)
        {
            this.card = card;
            this.assetProvider = assetProvider;
            _stackService = stackService;
        }

        public async void Enter(CardData payload)
        {
            card.SetAvailability(false,false,false);
            UpdateInfo(payload);
            await SetImage(payload);
            card.SetStack(_stackService.GetStack(payload.DeckType));
            PlayInitAnimation();
            card.StateMachine.Enter(CardState.inStuckOfCard);
        }

        public void Exit()
        {
            
        }

        private void UpdateInfo(CardData cardData)
        {
            card.UpdateData(cardData);
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