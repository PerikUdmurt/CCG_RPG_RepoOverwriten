using UnityEngine;

namespace CCG.Data
{
    [System.Serializable]
    public class PositionOnLevel
    {
        public string sceneName;
        public Vector3Data position;

        public PositionOnLevel(string initialLevel)
        {
            this.sceneName = initialLevel;
        }

        public PositionOnLevel(string sceneName,Vector3Data position) 
        {
            this.sceneName = sceneName;
            this.position = position;
        }
    }
}

