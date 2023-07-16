using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class StartSceneManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI highScoreText;

    private void Update()
    {
        highScoreText.text = "High Score: " + PlayerPrefs.GetInt("HighScore").ToString();

        if (Input.anyKeyDown)
        {
            SceneManager.LoadScene("GameScene");
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitGame();
        }
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