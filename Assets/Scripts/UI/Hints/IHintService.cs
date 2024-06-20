using UnityEngine;

namespace CCG.UI.Hints
{
    public interface IHintService
    {
        void CreateHint(string name, string hintText, Color color);
        void DeleteHint();
    }
}