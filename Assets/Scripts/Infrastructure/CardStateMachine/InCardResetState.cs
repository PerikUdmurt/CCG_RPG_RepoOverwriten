using CCG.Data;
using CCG.Services.SaveLoad;
using System.Collections.Generic;
using Zenject;

namespace CCG.Gameplay
{
    internal class InCardResetState : ICardState
    {
        private readonly ICard card;

        public InCardResetState(ICard card)
        {
            this.card = card;
        }
        public void Enter()
        {
            //Добавить карту в стопку сброса
            card.gameObject.transform.SetParent(card.gameObject.transform);
            card.gameObject.SetActive(false);
            card.StateMachine.Enter(CardState.inObjectPool);
        }

        public void Exit()
        {

        }
    }

    public class CardReset : IDataSaver, IDataLoader
    {
        private List<CardData> _cardResetList;

        [Inject]
        public CardReset()
        {
            
        }


        public void SaveData(ref GameData gameData)
        {
            gameData.cardReset = _cardResetList;
        }

        public void LoadData(GameData gamedata)
        {
            _cardResetList = gamedata.cardReset;
        }

        public void Add(ICard card)
        {
            CardData card = new CardData();
            _cardResetList.Add()
        }

        public void Remove()
        {

        }
    }
}