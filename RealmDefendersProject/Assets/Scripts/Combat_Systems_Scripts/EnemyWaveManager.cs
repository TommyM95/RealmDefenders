using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyWaveManager : MonoBehaviour
{
    public static EnemyWaveManager Instance { get; private set; }

    public event EventHandler OnWaveNumberChanged;

    private enum State
    {
        WaitingToSpawnNextWave,
        SpawningWave,
    }

    private State state;

    [SerializeField] private Transform enemyWavePosistionIndicatorTransform;
    [SerializeField] private List<Transform> spawnPositionTramsformList;
    private Vector3 spawnPosition;

    private float nextWaveSpawnTimer;
    [SerializeField] private float nextWaveSpawnTimerMax;
    private float nextEnemySpawnTimer;
    [SerializeField] private float nextEnemySpawnTimerMax;

    private int waveNumber;

    private int remaingEnemySpawnAmount;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        state = State.WaitingToSpawnNextWave;
        spawnPosition = spawnPositionTramsformList[UnityEngine.Random.Range(0, spawnPositionTramsformList.Count)].position;
        enemyWavePosistionIndicatorTransform.position = spawnPosition;
        nextWaveSpawnTimer = nextWaveSpawnTimerMax;
        nextEnemySpawnTimer = nextEnemySpawnTimerMax;
    }

    private void Update()
    {
        switch (state)
        {
            case State.WaitingToSpawnNextWave:
                nextWaveSpawnTimer -= Time.deltaTime;
                if (nextWaveSpawnTimer <= 0f && BuildingManager.Instance.GetPlayerCampBuilding() != null)
                {
                    SpawnWave();
                    nextWaveSpawnTimer = nextWaveSpawnTimerMax;
                }
                break;
            case State.SpawningWave:
                if (remaingEnemySpawnAmount > 0 && BuildingManager.Instance.GetPlayerCampBuilding() != null)
                {
                    nextEnemySpawnTimer -= Time.deltaTime;
                    if (nextEnemySpawnTimer < 0f)
                    {
                        nextEnemySpawnTimer = UnityEngine.Random.Range(0f, 0.2f);
                        Enemy.Create(spawnPosition + UtilitieClass.GetRandomDirection() * UnityEngine.Random.Range(0f, 10f));
                        remaingEnemySpawnAmount--;

                        if (remaingEnemySpawnAmount <= 0)
                        {
                            state = State.WaitingToSpawnNextWave;
                            spawnPosition = spawnPositionTramsformList[UnityEngine.Random.Range(0, spawnPositionTramsformList.Count)].position;
                            enemyWavePosistionIndicatorTransform.position = spawnPosition; 
                        }
                    }
                }
                break;
            default:
                break;
        }
    }

    private void SpawnWave()
    {
        nextWaveSpawnTimer = nextWaveSpawnTimerMax;
        remaingEnemySpawnAmount = 5 + 3 * waveNumber;
        state = State.SpawningWave;
        waveNumber++;
        OnWaveNumberChanged?.Invoke(this, EventArgs.Empty);
    }

    public int GetWaveNumber()
    {
        return waveNumber;
    }

    public float GetTimeTillNextWave()
    {
        return nextWaveSpawnTimer;
    }

    public Vector3 GetSpawnPosition()
    {
        return spawnPosition;
    }
}
