using CCG.Gameplay;
using CCG.Services.SaveLoad;
using CCG.StaticData.Cards;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CCG.Infrastructure.Factory
{
    public interface ISpawner
    {
        List<IDataSaver> DataSavers { get; }
        void CleanUp();
        Task CreateObjectPools();
        void DespawnCard(Card card);
        Task<Card> SpawnCardByStaticData(CardType cardType);
        void DespawnCardSlot(CardSlot slot);
        Task<CardSlot> SpawnCardSlot();
        Task<HandController> SpawnHand();
        void ReleaseObjectPools();
        Task<Card> SpawnCard(CardData cardData);
    }
}
