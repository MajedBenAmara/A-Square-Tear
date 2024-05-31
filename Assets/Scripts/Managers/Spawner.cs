using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private float _SpawnRate = 1f;
    [SerializeField] private GameObject[] _EnemiesToSpawn;
    [SerializeField] private Transform[] _SpawnPoints;
    private int _HighEnemyNumber = 0;
    private bool _BossIsAlive = false;
    void Start()
    {
        StartCoroutine(SpawnEnemies(_SpawnRate));        
    }

    // Spawning enemies based on the player level
    // enemy level also depend on the player level
    // the looping of the coroutine happens bc at the end of every coroutine it calls another one
    private IEnumerator SpawnEnemies(float _SpawnTime)
    {
        yield return new WaitForSeconds(_SpawnTime);
        Player player = GameManager.Instance.PlayerGameObject;
        int playerLevel = player.PlayerLvl;
        int enemyLevel = 1;
        if(playerLevel == 1)
        {
            GameObject newEnemy = Instantiate(_EnemiesToSpawn[enemyLevel-1], _SpawnPoints[Random.Range(0, _SpawnPoints.Length)].position, Quaternion.identity);
            StartCoroutine(SpawnEnemies(_SpawnTime));
        }
        else if(playerLevel == 2)
        {
            enemyLevel = 2;
            GameObject newEnemy = Instantiate(_EnemiesToSpawn[enemyLevel - 1], _SpawnPoints[Random.Range(0, _SpawnPoints.Length)].position, Quaternion.identity);
            StartCoroutine(SpawnEnemies(_SpawnTime));
        }
        else if(playerLevel == 3 ||  playerLevel == 4) 
        {
            enemyLevel = 3;
            int enemyIndex = Random.Range(enemyLevel - 2, enemyLevel);
            GameObject newEnemy = Instantiate(_EnemiesToSpawn[enemyIndex], _SpawnPoints[Random.Range(0, _SpawnPoints.Length)].position, Quaternion.identity);
            StartCoroutine(SpawnEnemies(_SpawnTime));
        }
        else if (playerLevel == 5)
        {
            enemyLevel = 3;
            GameObject newEnemy = Instantiate(_EnemiesToSpawn[enemyLevel - 1], _SpawnPoints[Random.Range(0, _SpawnPoints.Length)].position, Quaternion.identity);
            StartCoroutine(SpawnEnemies(_SpawnTime));
        }
        else if (playerLevel == 6 || playerLevel == 7)
        {
            enemyLevel = 4;
            int enemyIndex = Random.Range(enemyLevel - 2, enemyLevel);
            GameObject newEnemy = Instantiate(_EnemiesToSpawn[enemyIndex], _SpawnPoints[Random.Range(0, _SpawnPoints.Length)].position, Quaternion.identity);
            StartCoroutine(SpawnEnemies(_SpawnTime));
        }
        else if (playerLevel == 8)
        {
            enemyLevel = 4;
            GameObject newEnemy = Instantiate(_EnemiesToSpawn[enemyLevel - 1], _SpawnPoints[Random.Range(0, _SpawnPoints.Length)].position, Quaternion.identity);
            StartCoroutine(SpawnEnemies(_SpawnTime));
        }
        else if (playerLevel == 9)
        {
            // if the player kills 2 higher enemies the boss will respawn
            if (_HighEnemyNumber < 2)
            {
                _HighEnemyNumber++;
                enemyLevel = 5;
                GameObject newEnemy = Instantiate(_EnemiesToSpawn[enemyLevel - 1], _SpawnPoints[Random.Range(1, _SpawnPoints.Length)].position, Quaternion.identity);
                StartCoroutine(SpawnEnemies(4f));
            }
            else if (player.HigherEnemyKills >= 2)
            {
                enemyLevel = 6;
                // spawn the boss if he did not spawn yet
                if(_EnemiesToSpawn[enemyLevel - 1].name == "Boss" && _BossIsAlive == false)
                {
                    GameObject newEnemy = Instantiate(_EnemiesToSpawn[enemyLevel - 1], Vector2.zero, Quaternion.identity);
                    _BossIsAlive = true;
                    StartCoroutine(SpawnEnemies(_SpawnTime));
                }
                // if the boss has spawned then we create other small enemies to accompany him
                if (_BossIsAlive)
                {
                    enemyLevel = 4;
                    int enemyIndex = Random.Range(enemyLevel - 2, enemyLevel);
                    GameObject newEnemy = Instantiate(_EnemiesToSpawn[enemyIndex], _SpawnPoints[Random.Range(0, _SpawnPoints.Length)].position, Quaternion.identity);
                    StartCoroutine(SpawnEnemies(_SpawnTime));
                }
            }
            else
            {
                StartCoroutine(SpawnEnemies(_SpawnTime));

            }

        }

    }
}
