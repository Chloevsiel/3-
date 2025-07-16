using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverUI;

    void Start()
    {
        // ȷ�� gameOverUI ��ʼ����
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(false);
        }
        else
        {
            Debug.LogError("gameOverUI δ���䣡");
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
            Debug.LogError("gameOverUI δ���䣡");
        }
        Time.timeScale = 0f; // ��ͣ��Ϸ
        Debug.Log("��Ϸ��������ʾ GameOver UI");
    }

    public void Restart()
    {
        Debug.Log($"���¼��س�����{SceneManager.GetActiveScene().name}");
        Time.timeScale = 1f; // �ָ���Ϸʱ��
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void StartGame()
    {
        Debug.Log("���� Start ����");
        Time.timeScale = 1f;
        SceneManager.LoadScene("Start");
    }

    public void Quit()
    {
        Debug.Log("�˳���Ϸ");
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // �ڱ༭����ֹͣ����
#endif
    }
}