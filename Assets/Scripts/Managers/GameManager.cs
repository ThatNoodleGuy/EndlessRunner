using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Game Variables")]
    [SerializeField] private int score;
    private int highScore;
    [SerializeField] private int health;

    [Header("Game Texts")]
    [SerializeField] private TextMeshProUGUI scoreText = null;
    [SerializeField] private TextMeshProUGUI highScoreText = null;
    [SerializeField] private TextMeshProUGUI healthText = null;

    [Header("Game Over Screen")]
    [SerializeField] private GameObject gameOverGameObject = null;
    [SerializeField] private TextMeshProUGUI scoreTextgameOverCreen = null;
    [SerializeField] private TextMeshProUGUI highScoreTextgameOverScreen = null;
    private bool isGameEnded;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (PlayerPrefs.GetInt("HighScore") == 0)
        {
            PlayerPrefs.SetInt("HighScore", 0);
        }
    }

    private void Update()
    {
        if (!isGameEnded)
        {
            UpdateUI();
            HandlePlayerDeath();
        }

        if (isGameEnded)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene("GameScene");
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                PlayerPrefs.DeleteAll();
                SceneManager.LoadScene("StartScene");
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                QuitGame();
            }
        }
    }

    private void UpdateUI()
    {
        if (scoreText != null && healthText != null && highScoreText != null)
        {
            scoreText.text = score.ToString();
            healthText.text = health.ToString();
            highScoreText.text = PlayerPrefs.GetInt("HighScore").ToString();
        }
    }

    public void HandlePlayerDeath()
    {
        if (health <= 0)
        {
            health = 0;

            isGameEnded = true;
            PlayerController.instance.gameObject.SetActive(false);
            gameOverGameObject.SetActive(true);
            scoreTextgameOverCreen.text = "Score: " + score.ToString();
            highScoreTextgameOverScreen.text = "High Score: " + PlayerPrefs.GetInt("HighScore");
        }
    }

    public void AddScore()
    {
        score++;
    }

    public void TakeDamage()
    {
        health--;
    }

    public int GetScore()
    {
        return score;
    }

    public int GetHealth()
    {
        return health;
    }

    public bool GetIsGameEnden()
    {
        return isGameEnded;
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
        Input.backButtonLeavesApp = true;
    }
}