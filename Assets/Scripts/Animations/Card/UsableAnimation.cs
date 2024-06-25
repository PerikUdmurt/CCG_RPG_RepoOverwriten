using CCG.Gameplay.Hand;
using DG.Tweening;
using UnityEngine;

namespace CCG.Animation
{
    [RequireComponent(typeof(Usable))]
    public class UsableAnimation: MonoBehaviour
    {
        private IUsable usableObject;
        private void Awake()
        {
            if (gameObject.TryGetComponent<IUsable>(out IUsable usable))
            {
                usableObject = usable;
            }
        }

        private void OnEnable()
        {
            usableObject.Used += PlayUseAnimation;
        }

        private void OnDisable()
        {
            usableObject.Used -= PlayUseAnimation;
        }

        private void PlayUseAnimation()
        {
            Debug.Log("UseAnimation");
        }
    }
}