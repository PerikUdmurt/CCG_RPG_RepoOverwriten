using CCG.Gameplay;
using CCG.StaticData.Cards;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CCG.Services
{
    public class CardReciever : ICardReciever
    {
        public void CreateCardSlot()
        {
            throw new System.NotImplementedException();
        }

        public void DeleteCardSlot()
        {
            throw new System.NotImplementedException();
        }

        public List<ICard> GetCombination()
        {
            throw new System.NotImplementedException();
        }
    }

    public interface ICardReciever
    {
        void CreateCardSlot();
        void DeleteCardSlot();
        List<ICard> GetCombination();
    }
}
