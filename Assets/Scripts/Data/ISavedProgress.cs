using CCG.Data;

namespace CCG.Services.PersistentProgress
{
    public interface ISavedProgress
    {
        void UpdateProgress(PlayerProgress progress);
    }

    public interface ISavedProgressReader: ISavedProgress
    {
        void LoadProgress(PlayerProgress progress);
    }
}
