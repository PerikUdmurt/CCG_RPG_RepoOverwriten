using System.Threading.Tasks;
using UnityEngine;

namespace CCG.UI.Hints
{
    public interface IHintService
    {
        Task CreateHint(string name, string hintText, Color color);
        Task CreateObjectPool();
        void DeleteHint();
        void SetHintEntryPos(RectTransform hintEntryPos);
    }
}