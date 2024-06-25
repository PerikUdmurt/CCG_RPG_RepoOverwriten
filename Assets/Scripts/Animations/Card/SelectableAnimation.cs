using CCG.Gameplay.Hand;
using UnityEngine;

namespace CCG.Animation
{
    [RequireComponent(typeof(Selectable))]
    public class SelectableAnimation : MonoBehaviour
    {
        private ISelectable selectableObject;
        private void Awake()
        {
            if (gameObject.TryGetComponent<ISelectable>(out ISelectable usable))
            {
                selectableObject = usable;
            }
        }

        private void OnEnable()
        {
            selectableObject.Selected += PlaySelectAnimation;
            selectableObject.Deselected += PlayDeselectAnimation;
        }

        private void OnDisable()
        {
            selectableObject.Selected -= PlaySelectAnimation;
            selectableObject.Deselected -= PlayDeselectAnimation;
        }

        private void PlaySelectAnimation()
        {
            Debug.Log("SelectAnimation");
        }

        private void PlayDeselectAnimation()
        {
            Debug.Log("DeselectAnimation");
        }
    }
}
