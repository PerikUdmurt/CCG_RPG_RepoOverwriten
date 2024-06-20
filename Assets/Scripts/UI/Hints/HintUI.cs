using CCG.Infrastructure;
using TMPro;
using UnityEngine;

namespace CCG.UI.Hints
{
    public class HintUI : MonoBehaviour, ICustomPool
    {
        public TextMeshProUGUI hintName;
        public TextMeshProUGUI description;
        public RectTransform rectTransform;

        public void OnCreated()
        {
            gameObject.SetActive(false);
        }

        public void OnReceipt()
        {
            gameObject.SetActive(true);
        }

        public void OnReleased()
        {
            gameObject.SetActive(false);
        }

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
        }
    }
}
