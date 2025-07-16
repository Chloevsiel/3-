using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverUI;

    void Start()
    {
        // 确保 gameOverUI 初始隐藏
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(false);
        }
        else
        {
            Debug.LogError("gameOverUI 未分配！");
        }
    }

    public void GameOver()
    {
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);
        }
        else
        {
            Debug.LogError("gameOverUI 未分配！");
        }
        Time.timeScale = 0f; // 暂停游戏
        Debug.Log("游戏结束，显示 GameOver UI");
    }

    public void Restart()
    {
        Debug.Log($"重新加载场景：{SceneManager.GetActiveScene().name}");
        Time.timeScale = 1f; // 恢复游戏时间
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void StartGame()
    {
        Debug.Log("加载 Start 场景");
        Time.timeScale = 1f;
        SceneManager.LoadScene("Start");
    }

    public void Quit()
    {
        Debug.Log("退出游戏");
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // 在编辑器里停止播放
#endif
    }
}