using CCG.Gameplay;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace CCG.Services
{
    public interface ICardReciever
    {
        event Action Updated;
        void CleanUp();
        List<ICard> GetCombination();
        UniTask Start(int maxCount, int prepareSlot);
        UniTask Start(int maxCount, int prepareSlot, Vector3 atPosition);
    }
}
