using TMPro;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _LevelText, _HpText;
    [SerializeField] private UnityEngine.UI.Image _ExpBar, _HpBar;

    private float _ExpBarProgress;
    private float _PlayerCurrentExp, _ReqExpToLvlUp;
    private string _PlayerLevel;
    

    private void Update()
    {
        UpdateExpBar();
        UpdateHp();
    }

    // Updating ther different UI elements
    private void UpdateExpBar()
    {
        _PlayerCurrentExp = GameManager.Instance.PlayerGameObject.CurrentExp;
        _ReqExpToLvlUp = GameManager.Instance.ReqExpToLvlUp[GameManager.Instance.PlayerGameObject.PlayerLvl - 1];

        _PlayerLevel = GameManager.Instance.PlayerGameObject.PlayerLvl.ToString();
        _ExpBarProgress = _PlayerCurrentExp / _ReqExpToLvlUp;
        _ExpBar.fillAmount = _ExpBarProgress;
        _LevelText.text =" Lv " + _PlayerLevel; 
    }

    private void UpdateHp()
    {
        float playerHealth = GameManager.Instance.PlayerGameObject.Health;
        float playerMaxHealth = GameManager.Instance.PlayerGameObject.MaxHealth;
        _HpText.text = playerHealth + "/" + playerMaxHealth;
        float _HealthBarProgress = playerHealth / playerMaxHealth;

        _HpBar.fillAmount = _HealthBarProgress;
    }


}
