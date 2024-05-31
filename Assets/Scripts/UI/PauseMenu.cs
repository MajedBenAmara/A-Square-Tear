using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [HideInInspector] public bool IsShown = false;
    [SerializeField] private TextMeshProUGUI _PlayerLvlText, _HpText, _AtkText, _CritRateText, _CritDmgText, _SpeedText;
    [SerializeField] private GameObject _PausePanel, _OptionPanel, UpgradeMenu;
    private int _PlayerLvl, _Hp, _MaxHp;
    private float _Attack, _CritRate, _Critdmg, _Speed;
    private bool _OptionMenuIsActive;
    private Player _Player;


    
    void Update()
    {
        UpdateAnimation();
    }

    private void UpdatePlayerStats()
    {
        float _WeaponDmg = GameManager.Instance.GetSelectedWeapon()._WeaponDamage;
        _PausePanel.SetActive(false);
        _Player = GameManager.Instance.PlayerGameObject;
        _PlayerLvl = _Player.PlayerLvl;
        _Hp = _Player.Health;
        _MaxHp = _Player.MaxHealth;
        _Attack = _WeaponDmg;
        _CritRate = _Player.CritRate;
        _Critdmg = _Player.CritDmg;
        _Speed = _Player.MovementSpeed;

        _PlayerLvlText.text = _PlayerLvl.ToString();
        _HpText.text = _Hp.ToString() + "/" + _MaxHp.ToString();
        _AtkText.text = _Attack.ToString();
        _CritRateText.text = _CritRate.ToString();
        _CritDmgText.text = _Critdmg.ToString();
        _SpeedText.text = _Speed.ToString();
    }

    private void UpdateAnimation()
    {
        if (UpgradeMenu.activeInHierarchy == false)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (_OptionMenuIsActive)
                {
                    _OptionPanel.SetActive(false);
                    _OptionMenuIsActive = false;
                }
                else if (!IsShown)
                {
                    UpdatePlayerStats();
                    _PausePanel.SetActive(true);
                    Time.timeScale = 0f;
                    IsShown = true;
                }
                else
                {
                    Resume();
                }
            }
        }

    }

    public void Resume()
    {
        _PausePanel.SetActive(false);
        Time.timeScale = 1f;
        IsShown = false;
    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void OpenOptionMenu()
    {
        _OptionPanel.SetActive(true);
        _OptionMenuIsActive = true;
    }

    public void ExitMenu()
    {
        _OptionPanel.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
