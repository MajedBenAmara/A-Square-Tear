using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameMenu : MonoBehaviour
{
    [SerializeField] private GameObject _Panel;
    [SerializeField] private TextMeshProUGUI _ScoreText, _HighestScoreText;
    private GameObject _Player;
    private void Start()
    {
        _Player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        ActivateEndGamePanelWhenPlayerDead();
    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ActivateEndGamePanelWhenPlayerDead()
    {
        if (_Player.activeInHierarchy == false)
        {
            _Panel.SetActive(true);
        }
        int PlayerHighestScor = PlayerPrefs.GetInt("Player Highest Score");
        _HighestScoreText.text = "Score: " + PlayerHighestScor.ToString();
        _ScoreText.text = "Highest Score: " + _Player.GetComponent<Player>().PlayerScore.ToString();
    }
    public void ActivateEndGamePanel()
    {
        _Panel.SetActive(true);
        int PlayerHighestScor = PlayerPrefs.GetInt("Player Highest Score");
        _HighestScoreText.text = "Score: " + PlayerHighestScor.ToString();
        _ScoreText.text = "Highest Score: " + _Player.GetComponent<Player>().PlayerScore.ToString();
        Time.timeScale = 0f;
    }
}
