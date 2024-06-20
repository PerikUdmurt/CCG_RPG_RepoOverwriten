using CCG.Infrastructure.Factory;
using CCG.Services.FileData;
using System.Collections.Generic;
using UnityEngine;

namespace CCG.Services.SaveLoad
{
    public class DataPersistenceService : IDataPersistentService
    {
        private GameData gameData;

        private List<IDataSaver> dataSavers = new List<IDataSaver>();

        private FileDataService fileDataService;
        private readonly ISpawner GameSpawner;

        public GameData GameData { get { return gameData; } }

        public DataPersistenceService(ISpawner gameSpawner)
        {
            GameSpawner = gameSpawner;
            fileDataService = new FileDataService(Application.persistentDataPath, "TestData.json");
        }

        public void NewGame()
        {
            gameData = new GameData();
        }

        public void LoadGame()
        {
            gameData = fileDataService.Load();
            if (gameData == null)
            {
                Debug.Log("Игровые данные не найдены. Созданы новые игровые данные");
                NewGame();
            }
        }

        public void SaveGame()
        {
            foreach (IDataSaver saver in dataSavers)
            {
                saver.SaveData(ref gameData);
            }

            foreach (IDataSaver saver in GameSpawner.DataSavers)
            {
                saver.SaveData(ref gameData);
            }
            fileDataService.Save(gameData);
        }
    }

    public interface IDataSaver
    {
        void SaveData(ref GameData gameData);
    }

    public interface IDataLoader
    {
        void LoadData(GameData gamedata);
    }
}
