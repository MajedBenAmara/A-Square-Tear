using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    [SerializeField] private GameObject _OptionPanel, _WeaponsPanel;
    private bool _OptionMenuIsActive = false, _WeaponMenuIsActive = false;

    private void Update()
    {
        UpdateAnimation();
    }
    private void UpdateAnimation()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_OptionMenuIsActive)
            {
                _OptionPanel.SetActive(false);
                _OptionMenuIsActive = false;
            }

            if (_WeaponMenuIsActive)
            {
                _WeaponsPanel.SetActive(false);
                _WeaponMenuIsActive = false;
            }
        }
    }

    public void ShowWeaponPanel()
    {
        _WeaponsPanel.SetActive(true);
        _WeaponMenuIsActive = true;
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

    public void ExitWeaponPanel()
    {
        _WeaponsPanel.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
