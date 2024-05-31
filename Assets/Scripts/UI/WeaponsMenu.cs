using UnityEngine;

public class WeaponsMenu : MonoBehaviour
{
    private int _Index = 0;
    [SerializeField] private RectTransform[] _Weapons = { };


    private void Start()
    {
        PlayerPrefs.SetInt("SelectedWeapon", 0);
        _Weapons[0].gameObject.SetActive(true);

    }

    // changing the weapon by activating it's game object and desactivating the other ones bes on there index
    public void ChangeWeapon(bool right)
    {
        if (right)
        {
            _Index++;
            PlayerPrefs.SetInt("SelectedWeapon", _Index);
            if (_Index >= _Weapons.Length)
            {
                _Index = 0;
                PlayerPrefs.SetInt("SelectedWeapon", _Index);
                _Weapons[_Weapons.Length - 1].gameObject.SetActive(false);
                _Weapons[_Index].gameObject.SetActive(true);
            }
            else
            {
                _Weapons[_Index - 1].gameObject.SetActive(false);
                _Weapons[_Index].gameObject.SetActive(true);
            }


        }
        else
        {
            _Index--;
            PlayerPrefs.SetInt("SelectedWeapon", _Index);
            if (_Index < 0)
            {
                _Index = _Weapons.Length - 1;
                PlayerPrefs.SetInt("SelectedWeapon", _Index);
                _Weapons[0].gameObject.SetActive(false);
                _Weapons[_Index].gameObject.SetActive(true);
            }
            else
            {
                _Weapons[_Index + 1].gameObject.SetActive(false);
                _Weapons[_Index].gameObject.SetActive(true);
            }

        }
    }
}
