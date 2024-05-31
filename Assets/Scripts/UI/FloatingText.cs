using TMPro;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    [SerializeField] private Color _TextColor, _CritTextColor;
    [SerializeField] private TMPro.TMP_FontAsset _TextFont;
    [SerializeField] private GameObject _FloatingText;
    [SerializeField] private float _FloatingTextFontSize = 6f;
    private bool _WeaponDidCrit;
    [HideInInspector] public string _TextInput;

    private void Start()
    {
        _WeaponDidCrit = GameManager.Instance.GetWeaponCritCondition();
        _FloatingText.GetComponent<TextMeshPro>().text = _TextInput;
        Debug.Log("Weapon Did Crit = " + _WeaponDidCrit);

        if (_WeaponDidCrit)
        {
            _FloatingText.GetComponent<TextMeshPro>().color = _CritTextColor;
            _FloatingText.GetComponent<TextMeshPro>().fontSize += _FloatingText.GetComponent<TextMeshPro>().fontSize * .5f;
        }
        else
        {
            _FloatingText.GetComponent<TextMeshPro>().color = _TextColor;
            _FloatingText.GetComponent<TextMeshPro>().fontSize = _FloatingTextFontSize;
        }

        _FloatingText.GetComponent<TextMeshPro>().font = _TextFont;
        _FloatingText.GetComponent<Animator>().Play("Floating_Text");
        Destroy(gameObject, .5f);
    }

}
