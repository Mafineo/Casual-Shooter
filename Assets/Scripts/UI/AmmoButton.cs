using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game.UI
{
    public class AmmoButton : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private Image icon;
        [SerializeField] private Image shadow;
        [SerializeField] private GameObject selection;

        public void Init(Sprite image, UnityAction call)
        {
            button.onClick.RemoveAllListeners();
            icon.sprite = shadow.sprite = image;
            button.onClick.AddListener(call);
        }

        public void SetSelectionVisibility(bool value)
        {
            selection.SetActive(value);
        }

        private void OnDestroy()
        {
            if (button != null)
            {
                button.onClick.RemoveAllListeners();
            }
        }
    }
}