using UnityEngine;

namespace CCG.Gameplay
{
    public interface IMovable
    {
        void MoveTo(Vector3 endPoint);
        void MoveToParent(Transform parent);
    }
}
