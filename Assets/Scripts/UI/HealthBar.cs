using UnityEngine.UI;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private Slider _Slider;

    private void Start()
    {
        _Slider = GetComponent<Slider>();
    }
    void Update()
    {
        HealthBarUpdate();
    }

    private void HealthBarUpdate()
    {
        float playerHealth = GameManager.Instance.PlayerGameObject.Health;
        float playerMaxHealth = GameManager.Instance.PlayerGameObject.MaxHealth;
        float _HealthBarProgress = playerHealth / playerMaxHealth;

         _Slider.value = _HealthBarProgress;    
    }
}
