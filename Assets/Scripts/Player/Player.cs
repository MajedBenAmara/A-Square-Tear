using UnityEngine;

public class Player : Fighter
{
    [SerializeField] private float _PosXConstrain = 77f, _PosYConstrain = 33f, _NegXConstrain= -77f, _NegYConstrain = -27f;
    
    private Rigidbody2D _Rb;
    private float _XCoor, _YCoor;

    public int HigherEnemyKills = 0, PlayerScore = 0, PlayerHighestScor = 0;
    public int CurrentExp = 0, PlayerLvl = 1;
    public int CritRate = 0, CritDmg = 0;
    public int MaxLevel = 5, MaxHealth;

    protected override void Start()
    {
        base.Start();
        PlayerHighestScor = PlayerPrefs.GetInt("Player Highest Score");
        _Rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        _XCoor = Input.GetAxisRaw("Horizontal");
        _YCoor = Input.GetAxisRaw("Vertical");
        CheckPosition();
    }

    private void FixedUpdate()
    {
        _Rb.velocity = new Vector2(_XCoor, _YCoor).normalized * MovementSpeed;
    }

    // if the player is enable\active then we start the experience algorith(how the player gain experience and level up) if not we stop the algorithm
    //////////////////////////////////////////////////////////
    private void OnEnable()
    {
        GameManager.Instance.OnExperienceChange += GameManager.Instance.HandleExperienceChange;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnExperienceChange -= GameManager.Instance.HandleExperienceChange;

    }
    //////////////////////////////////////////////////////////

    //  definig what happen when the player is dead
    protected override void Death()
    {
        if(PlayerScore >= PlayerHighestScor)
        {
            PlayerHighestScor = PlayerScore;
            PlayerPrefs.SetInt("Player Highest Score", PlayerHighestScor);
        }
        Time.timeScale = 0;
        gameObject.SetActive(false);
    }

    private void CheckPosition()
    {
        if ((transform.position.y > _NegYConstrain && transform.position.y < _PosYConstrain) && (transform.position.x > _NegXConstrain && transform.position.x < _PosXConstrain))
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        else
        {
            if (transform.position.y <= _NegYConstrain)
                transform.position = new Vector3(transform.position.x, _NegYConstrain, transform.position.z);
            if (transform.position.y >= _PosYConstrain)
                transform.position = new Vector3(transform.position.x, _PosYConstrain, transform.position.z);
            if (transform.position.x <= _NegXConstrain)
                transform.position = new Vector3(_NegXConstrain, transform.position.y, transform.position.z);
            if (transform.position.x >= _PosXConstrain)
                transform.position = new Vector3(_PosXConstrain, transform.position.y, transform.position.z);
        }
    }

    // Upgrade the diffrent stats of the player : crite rate/dmg, movement speed, max health
    ////////////////////////////////////////////////////////////
    public void UpgradeCriteRate(int criteBonus)
    {
        CritRate += criteBonus;
    }

    public void UpgradeCriteDmg(int criteBonus)
    {
        CritDmg += criteBonus;
    }

    public void UpgradeMovementSpeed(int speed)
    {
        MovementSpeed += speed;
    }

    public void UpgradeMaxHealth(int maxHealthBonus)
    {
        MaxHealth += maxHealthBonus;
    }
    ////////////////////////////////////////////////////////////
    
    // this function will be used in the spawner scripte to help with the logic of spawning the different enemy types
    public void IncreaseHigherEnemyScore()
    {
        HigherEnemyKills++;
    }



}
