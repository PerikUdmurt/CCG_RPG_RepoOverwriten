using CCG.Data;
using CCG.Infrastructure.Factory;
using CCG.Services.PersistentProgress;
using UnityEngine;

namespace CCG.Services.SaveLoad
{
    public class SavedLoadService : ISavedLoadService
    {
        private const string ProgressKey = "Progress";
        private ISpawner _gameFactory;
        private IPersistentProgressService _persistentProgressService;

        public SavedLoadService(ISpawner factory, IPersistentProgressService persistentProgressService) 
        { 
            _gameFactory = factory;
            _persistentProgressService = persistentProgressService;
        }

        public PlayerProgress LoadProgress()
            {
                 return PlayerPrefs.GetString(ProgressKey)?
                .ToDeserialized<PlayerProgress>();
            }
        public void SaveProgress()
        {
            foreach (ISavedProgress progressWriter in _gameFactory.ProgressWriters)
            {
                progressWriter.UpdateProgress(_persistentProgressService.playerProgress);

                PlayerPrefs.SetString(ProgressKey, _persistentProgressService.playerProgress.ToJson()); 
            }
        }
    }
}