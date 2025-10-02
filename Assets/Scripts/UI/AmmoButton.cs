using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game.UI
{
    public class AmmoButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private Image _image;
        [SerializeField] private Image _shadow;
        [SerializeField] private GameObject _selection;

        public bool IsSelected
        {
            set
            {
                _selection.SetActive(value);
            }
        }

        public void Init(Sprite image, UnityAction call)
        {
            _button.onClick.RemoveAllListeners();
            _image.sprite = _shadow.sprite = image;
            _button.onClick.AddListener(call);
        }
    }
}