using CCG.StaticData.Cards;

namespace CCG.Services
{
    public interface ICardStaticDataService
    {
        CardStaticData GetStaticData(CardType type);
        void LoadModules();
    }
}