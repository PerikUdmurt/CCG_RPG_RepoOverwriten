using CCG.Data;

namespace CCG.Services.SaveLoad
{
    public interface ISavedLoadService
    {
        void SaveProgress();
        PlayerProgress LoadProgress();
    }
}