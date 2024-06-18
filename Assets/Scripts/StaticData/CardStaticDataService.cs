using CCG.Infrastructure.AssetProvider;
using CCG.StaticData.Cards;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CCG.Services
{
    public class CardStaticDataService : ICardStaticDataService
    {
        private Dictionary<CardType, CardStaticData> _modules;

        public void LoadModules()
        {
            _modules = Resources.LoadAll<CardStaticData>(AssetPath.AllCardStaticData)
                .ToDictionary(x => x.CardID, x => x);
        }

        public CardStaticData GetStaticData(CardType type)
        {
            CardStaticData data = _modules.TryGetValue(type, out CardStaticData value) ? value : null;
            return data;
        }
    }
}

