using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class EnemyWaveUI : MonoBehaviour
{

    [SerializeField] private EnemyWaveManager enemyWaveManager;

    private Camera mainCamera;

    private TextMeshProUGUI waveNumberText;
    private TextMeshProUGUI waveMessageText;

    private RectTransform waveSpawnPosArrow;

    private void Awake()
    {
        waveNumberText = transform.Find("waveNumberText").GetComponent<TextMeshProUGUI>();
        waveMessageText = transform.Find("waveMessageText").GetComponent<TextMeshProUGUI>();
        waveSpawnPosArrow = transform.Find("WaveSpawnPosArrow").GetComponent<RectTransform>();
    }

    private void Start()
    {
        mainCamera = Camera.main;
        enemyWaveManager.OnWaveNumberChanged += EnemyWaveManager_OnWaveNumberChanged;
        SetWaveNumberText("Wave : " + enemyWaveManager.GetWaveNumber());
    }

    private void EnemyWaveManager_OnWaveNumberChanged(object sender, EventArgs e)
    {
        SetWaveNumberText("Wave : " + enemyWaveManager.GetWaveNumber());
    }

    private void Update()
    {
        NextWaveMessage();

        EnemyWaveSpawnPositionIndicator();
    }

    private void NextWaveMessage() 
    {
        float nextWaveSpawnTimer = enemyWaveManager.GetTimeTillNextWave();
        if (nextWaveSpawnTimer <= 0f)
        {
            SetWaveMessageText("");
        }
        else
        {
            SetWaveMessageText("Next Wave : " + nextWaveSpawnTimer.ToString("F1") + "s");
        }
    }

    private void EnemyWaveSpawnPositionIndicator()
    {
        Vector3 dirToNextSpawnPosition = (enemyWaveManager.GetSpawnPosition() - mainCamera.transform.position).normalized;

        waveSpawnPosArrow.anchoredPosition = dirToNextSpawnPosition * 300f;
        waveSpawnPosArrow.eulerAngles = new Vector3(0, 0, UtilitieClass.GetAngleFromVector(dirToNextSpawnPosition));

        float distanceToNextSpawnPosition = Vector3.Distance(enemyWaveManager.GetSpawnPosition(), mainCamera.transform.position);
        waveSpawnPosArrow.gameObject.SetActive(distanceToNextSpawnPosition > mainCamera.orthographicSize * 1.5F);
    }

    private void SetWaveMessageText(string message)
    {
        waveMessageText.SetText(message);
    }

    private void SetWaveNumberText(string text)
    {
        waveNumberText.SetText(text);
    }
}
