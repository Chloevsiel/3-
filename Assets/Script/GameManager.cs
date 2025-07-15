using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverUI;

    public void GameOver()
    {
        gameOverUI.SetActive(true);
        Time.timeScale = 0f; // ÔÝÍ£ÓÎÏ·
        gameOverUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Restart()
    {
        Time.timeScale = 1f; // »Ö¸´ÓÎÏ·
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void StartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Start");
    }

    public void Quit()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // ÔÚ±à¼­Æ÷ÀïÍ£Ö¹²¥·Å
#endif
    }
}