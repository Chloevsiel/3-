using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverUI;
    public bool isGameOver = false;

    void Awake()
    {
        Time.timeScale = 1f; // 保证每次启动时游戏是运行状态
    }

    public void gameOver()
    {
        isGameOver = true;
        gameOverUI.SetActive(true);
        Time.timeScale = 0f; // 暂停游戏
    }

    public void restart()
    {
        Time.timeScale = 1f; // 恢复时间
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void start()
    {
        Time.timeScale = 1f; // 防止从暂停状态开始游戏
        SceneManager.LoadScene("Start");
    }

    public void quit()
    {
        Application.Quit();
    }
}
