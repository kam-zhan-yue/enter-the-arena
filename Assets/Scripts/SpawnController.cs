/*
 * Author: Alex Kam
 * Date: 2-8-19
 * Licence: Unity Personal Editor Licence
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    public static SpawnController SC;
    public GameObject playerPrefab;
    
    public Transform playerSpawn;
    public Transform[] enemySpawn;

    public enum SpawnState { SPAWNING, WAITING, COUNTING };

    [System.Serializable]
    public class Wave
    {   
        public string name;
        public int enemyNum;
        public Transform enemy;
        public int bossNum;
        public Transform boss;
        public float rate;
    }
    public Wave[] waves;
    public int nextWave = 0;
    int spawnPicker;

    public float timeBetweenWaves = 5f;
    public float waveCountdown = 0f;
    public int wavesCompleted = 0;

    private float searchCountdown = 1f;

    public float multiplier = 1;

    private SpawnState state = SpawnState.COUNTING;

    private void Awake()
    {
        if (SpawnController.SC == null)
            SpawnController.SC = this;
    }

    void Start()
    {
        if(enemySpawn.Length ==0)
            Debug.LogError("No spawn points are referenced");
        CreatePlayer(playerPrefab);
        waveCountdown = timeBetweenWaves;
    }

    //===============PROCEDURE===============//
    void CreatePlayer(GameObject player)
    //Purpose:          Instantiates a player game object into the arena
    //GameObject player: Stores the player prefab
    {
        Instantiate(player, new Vector3(0, 0, 0), Quaternion.identity);
    }

    void Update()
    {
        if(state == SpawnState.WAITING)
        {
            if(!EnemyIsAlive())
            {
                //Begin a new Wave
                WaveCompleted();
                if (wavesCompleted % 3 == 0)
                {
                    RegenHP();
                }
                return;
            }
            else
            {
                return;
            }
        }

        if (waveCountdown <= 0)
        {
            if(state != SpawnState.SPAWNING)
            {
                StartCoroutine(SpawnWave(waves[nextWave],multiplier));
            }
        }
        else
        {
            CountDown();
        }
    }

    //===============PROCEDURE===============//
    void CountDown()
    //Purpose:          Deducts waveCountdown by the time
    {
        waveCountdown -= Time.deltaTime;
    }

    //===============PROCEDURE===============//
    void RegenHP()
    //Purpose:          Regens the player's health to 100%
    {
        PlayerController.healthAmount = PlayerController.maxHealth;
        AudioManager.AM.Play("HP");
    }

    //===============PROCEDURE===============//
    void WaveCompleted()
    //Purpose:          Sets the state to COUNTING and increments the wave and sometimes multiplier
    {
        Debug.Log("Wave Completed");
        state = SpawnState.COUNTING;
        waveCountdown = timeBetweenWaves;
        if(nextWave+1 > waves.Length-1)
        {
            nextWave = -1;
            multiplier++;
            Debug.Log("All Waves Completed! Looping...");
        }
        wavesCompleted++;
        nextWave++;
    }

    //===============FUNCTION===============//
    bool EnemyIsAlive()
    //Purpose:          Checks whether any enemies are alive every so often
    {
        searchCountdown -= Time.deltaTime;
        if (searchCountdown <= 0f)
        {
            if (GameObject.FindGameObjectWithTag("Enemy") == null && GameObject.FindGameObjectWithTag("Boss") == null)
                return false;
        }
        return true;
    }

    //===============PROCEDURE===============//
    IEnumerator SpawnWave(Wave _wave, float difficulty)
    //Purpose:          Spawns a wave of enemies and bosses through instantiation
    //Wave _wave:    Stores the number of enemies and bosses to be spawned
    //int difficulty:       Determines the multiplier of enemies to be spawned
    {
        Debug.Log("Spawning Wave: " + _wave.name);
        state = SpawnState.SPAWNING;
        for(int i =0; i<_wave.enemyNum*difficulty; i++)
        {
            SpawnEnemy(_wave.enemy);
            yield return new WaitForSeconds(1f / _wave.rate);
        }

        if (waves[nextWave].boss != null)
        {
            for (int i = 0; i < _wave.bossNum*difficulty; i++)
            {
                SpawnBoss(_wave.boss);
                yield return new WaitForSeconds(1f / _wave.rate);
            }
        }
        state = SpawnState.WAITING;
        yield break;
    }

    //===============PROCEDURE===============//
    void SpawnEnemy(Transform _enemy)
    //Purpose:          Spawns an enemy
    //Transform _enemy: Stores the enemy prefab
    {
        Debug.Log("Spawning Enemy: " + _enemy.name);
        spawnPicker = Random.Range(0, enemySpawn.Length);
        Instantiate(_enemy, enemySpawn[spawnPicker].position, enemySpawn[spawnPicker].rotation);
    }

    //===============PROCEDURE===============//
    void SpawnBoss(Transform _boss)
    //Purpose:          Spawns a boss
    //Transform _boss: Stores the boss prefab
    {
        Debug.Log("Spawning Boss: " + _boss.name);
        spawnPicker = Random.Range(0, enemySpawn.Length);
        Instantiate(_boss, enemySpawn[spawnPicker].position, enemySpawn[spawnPicker].rotation);
    }
}
