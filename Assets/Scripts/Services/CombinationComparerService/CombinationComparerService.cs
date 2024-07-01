

using CCG.Gameplay;
using CCG.StaticData.Cards;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CCG.Services.Combination
{
    public class CombinationComparerService: ICombinationComparerService
    {
        private readonly ICardReciever _cardReciever;

        public CombinationComparerService(ICardReciever cardReciever) 
        {
            _cardReciever = cardReciever;
            _cardReciever.Updated += Compare;
        }

        public void SetCombinations(List<CardType> combinations)
        {
            CleanUp();
        }

        private void CleanUp()
        {

        }

        private void Compare()
        {

        }

        private Tuple<List<CardType>,int> ConvertToCombination()
        {
            List<ICard> cards = _cardReciever.GetCombination();
            var valuelist = from card in cards select card.ValueOfCard;
            var value = valuelist.Sum();
            var list = cards.Distinct().OrderBy(x => x.CardID).Select(x => x.CardID).ToList();
            return new Tuple<List<CardType>, int>(list, value);
        }
    }

    public interface ICombinationComparerService
    {

    }
}


