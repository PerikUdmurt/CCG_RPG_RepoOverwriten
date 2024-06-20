namespace CCG.Services.SaveLoad
{
    public interface IDataPersistentService
    {
        GameData GameData { get; }

        void LoadGame();
        void NewGame();
        void SaveGame();
    }
}
