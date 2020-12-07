using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOverUI : MonoBehaviour
{
    public static GameOverUI Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        transform.Find("retryButton").GetComponent<Button>().onClick.AddListener(() => {
            GameSceneManager.Load(GameSceneManager.Scene.GameScene);
        });
        transform.Find("mainMenuButton").GetComponent<Button>().onClick.AddListener(() => {
            GameSceneManager.Load(GameSceneManager.Scene.MainMenuScene);
        });

        Hide();
    }

    public void Show()
    {
        gameObject.SetActive(true);

        transform.Find("wavesLastedText").GetComponent<TextMeshProUGUI>().SetText("You lasted " + EnemyWaveManager.Instance.GetWaveNumber() + " Waves!");
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
