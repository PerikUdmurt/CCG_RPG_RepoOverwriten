using System;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using System.Threading.Tasks;
using System.Threading;
using UnityEngine.UI;

namespace CCG.UI
{
    public class MyButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        private const int DefaultLongPressTime = 1000;
        
        public bool isInteractable = true;

        public event Action Clicked;
        public event Action Unclicked;
        public event Action LongPressed;
        public event Action LongPressedCancelled;
        
        [SerializeField] private int _longPressTime;
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private TextMeshProUGUI _buttonText;
        [SerializeField] private Image _image;

        private CancellationTokenSource _cts = new CancellationTokenSource();
        private CancellationToken _token;
        private bool _pressed;

        public int LongPressTime
        {
            get { return _longPressTime; }
            set 
            { 
                _longPressTime = value;
                if (_longPressTime < 0) { _longPressTime = DefaultLongPressTime; }
            }
        }

        public RectTransform RectTrans => _rectTransform;

        public TextMeshProUGUI ButtonText { get; set; }

        public Image ButtonImage { get; set; }

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _image = GetComponent<Image>();
            _buttonText = GetComponentInChildren<TextMeshProUGUI>();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (!isInteractable) return;
            Clicked?.Invoke();
            _cts.Dispose();
            _cts = new CancellationTokenSource();
            _token = _cts.Token;
            _token.Register(() => { CancelLongPress(); });
            ConfirmLongPress();
            _pressed = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (!isInteractable) return;
            Unclicked?.Invoke();
            _cts.Cancel();
        }

        private async Task<bool> LongPress(CancellationToken token)
        {
            if (!isInteractable) return false;
            await Task.Delay(_longPressTime);
            if (token.IsCancellationRequested) return false;
           
            return true;
        }

        private async void ConfirmLongPress()
        {
            bool isConfirmed = await LongPress(_token);
            if (isConfirmed)
            {
                LongPressed?.Invoke();
                _pressed = false;
            }
        }

        private void CancelLongPress()
        {
            if (!_pressed) return;
            LongPressedCancelled?.Invoke();
            _pressed = false;
        }
    }
}
