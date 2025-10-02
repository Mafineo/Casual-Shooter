using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Game.Enitites;

namespace Game.UI
{
    public class AmmoSelector : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private Weapon _weapon;
        [Header("UI")]
        [SerializeField] private Transform _content;
        [SerializeField] private AmmoButton _buttonPrefab;

        private List<AmmoButton> _buttons = new List<AmmoButton>();
        private int _selectedAmmoId = 0;

        private void Awake()
        {
            LoadAmmoVariants(_weapon);
            SelectAmmo(_weapon, 0);
        }

        private void LoadAmmoVariants(Weapon weapon)
        {
            foreach(AmmoButton ammoButton in _buttons)
            {
                Destroy(ammoButton.gameObject);
            }
            _buttons.Clear();
            Bullet[] bulletList = weapon.BulletList;
            for (int i = 0; i < bulletList.Length; i++)
            {
                AmmoButton ammoButton = Instantiate(_buttonPrefab, _content.transform);
                int temp = i;
                ammoButton.Init(bulletList[i].Icon, delegate { SelectAmmo(weapon, temp);  });
                _buttons.Add(ammoButton);
            }
        }

        private void SelectAmmo(Weapon weapon, int id)
        {
            if (weapon.LoadBullet(id))
            {
                _buttons[_selectedAmmoId].IsSelected = false;
                _selectedAmmoId = id;
                _buttons[_selectedAmmoId].IsSelected = true;
            }
        }
    }
}