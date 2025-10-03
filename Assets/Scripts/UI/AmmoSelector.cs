using System.Collections.Generic;
using UnityEngine;
using Game.Enitites.Weapons;
using Game.Enitites.Bullets;

namespace Game.UI
{
    public class AmmoSelector : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private Weapon weapon;
        [Header("UI")]
        [SerializeField] private Transform content;
        [SerializeField] private AmmoButton buttonPrefab;

        private List<AmmoButton> _buttons = new List<AmmoButton>();
        private AmmoButton _selectedButton;

        private void Awake()
        {
            Init(weapon);
            SelectAmmo(weapon, 0);
        }

        private void Init(Weapon weapon)
        {
            ClearAmmoVariants();
            LoadAmmoVariants(weapon);
        }

        private void ClearAmmoVariants()
        {
            foreach (AmmoButton ammoButton in _buttons)
            {
                Destroy(ammoButton.gameObject);
            }
            _buttons.Clear();
        }

        private void LoadAmmoVariants(Weapon weapon)
        {
            Bullet[] bulletList = weapon.BulletList;
            for (int i = 0; i < bulletList.Length; i++)
            {
                AmmoButton ammoButton = Instantiate(buttonPrefab, content.transform);
                int temp = i;
                ammoButton.Init(bulletList[i].Icon, delegate { SelectAmmo(weapon, temp); });
                _buttons.Add(ammoButton);
            }
        }

        private void SelectAmmo(Weapon weapon, int id)
        {
            if (weapon.LoadBullet(id))
            {
                if (_selectedButton != null)
                {
                    _selectedButton.SetSelectionVisibility(false);
                }
                _selectedButton = _buttons[id];
                _selectedButton.SetSelectionVisibility(true);
            }
        }
    }
}