using DG.Tweening;
using UnityEngine;

namespace CCG.Gameplay
{
    [RequireComponent(typeof(Transform))]
    public class Movable : MonoBehaviour, IMovable
    {
        [SerializeField] private float _moveSpeed;

        public void MoveTo(Vector3 endPoint)
        {
                DOTween.Sequence().
                Append(transform.DOMove(endPoint, _moveSpeed, false).
                SetEase(Ease.OutQuint));
        }

        public void MoveToParent(Transform parent)
        {
            MoveTo(parent.position);
            transform.SetParent(parent);
        }
    }

}
