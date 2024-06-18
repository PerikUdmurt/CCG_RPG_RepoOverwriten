using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CCG.Data
{
    public class PlayerProgress 
    {
        public WorldData WorldData;

        public PlayerProgress(string initialLevel)
        {
            WorldData = new WorldData(initialLevel);
        }
    }

    public class PersistentProgressService: IPersistentProgressService
    {
        public PlayerProgress playerProgress {  get; set; } 
    }

    public interface IPersistentProgressService
    {
        public PlayerProgress playerProgress { get; set; }
    }
}

