using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyToSpawn;       // Enemy prefab to spawn

    [SerializeField]
    private float _delay = 2f;      // Time between spawns

    private float _canSpawn = 2f;   // Timer to tell when time's up

    private void FixedUpdate()
    {
        if (_canSpawn <= 0 && GameManager.numEnemies < 300 && !GameManager.gameOver)     // If the timer is up, there's currently less than 300 enemies, and the game is still going
        {
            SpawnEnemy();
            _canSpawn = _delay;
        }

        else
        {
            if(_canSpawn > 0)
            {
                _canSpawn -= Time.deltaTime;
            }
        }
    }

    private void SpawnEnemy()
    {
        Instantiate(_enemyToSpawn, transform.position, transform.rotation);     // Spawn and enemy at current transform
        GameManager.numEnemies++;       // Increase enemy count to keep track of them
        //Debug.Log(GameManager.numEnemies);
    }
}
