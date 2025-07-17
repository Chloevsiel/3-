using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;

public class ButtonManager : MonoBehaviour
{
    public Button startButton;
    public Button scoreButton;
    public Button exitButton;
    public GameObject manualPanel;
    public GameObject leaderboardPanel;
    public Text leaderboardText;
    public Text bestTimeText;
    public Button clearButton;

    private const int MAX_ENTRIES = 5;

    void Start()
    {
        if (startButton != null) startButton.onClick.AddListener(ShowManualAndHideButtons);
        if (scoreButton != null) scoreButton.onClick.AddListener(ShowLeaderboard);
        if (exitButton != null) exitButton.onClick.AddListener(QuitGame);

        if (manualPanel != null) manualPanel.SetActive(false);
        if (leaderboardPanel != null) leaderboardPanel.SetActive(false);

        InitializeUI(); // 如果你后续要扩展初始化，保留这个调用
        UpdateBestTimeDisplay(); // 显示 Best Time

        if (clearButton != null)
        {
            clearButton.onClick.AddListener(ClearLeaderboard);
        }
        else
        {
            Debug.LogWarning("Clear button not assigned.");
        }
    }

    void InitializeUI()
    {
        // 目前没有额外逻辑，这里可以保留为空或扩展
    }

    public void ShowManualAndHideButtons()
    {
        startButton.gameObject.SetActive(false);
        scoreButton.gameObject.SetActive(false);
        exitButton.gameObject.SetActive(false);

        if (manualPanel != null)
        {
            manualPanel.SetActive(true);
            Button closeBtn = manualPanel.GetComponentInChildren<Button>();
            if (closeBtn != null)
            {
                closeBtn.onClick.RemoveAllListeners();
                closeBtn.onClick.AddListener(() =>
                {
                    manualPanel.SetActive(false);
                    SceneManager.LoadScene("Stage1");
                });
            }
        }
    }

    public void ShowLeaderboard()
    {
        if (leaderboardPanel != null)
        {
            leaderboardPanel.SetActive(true);
            UpdateLeaderboardDisplay();

            Button closeBtn = leaderboardPanel.GetComponentInChildren<Button>();
            if (closeBtn != null)
            {
                closeBtn.onClick.RemoveAllListeners();
                closeBtn.onClick.AddListener(() =>
                {
                    leaderboardPanel.SetActive(false);
                });
            }
        }
    }

    void UpdateLeaderboardDisplay()
    {
        if (leaderboardText == null) return;

        var times = LoadTimes();
        string displayText = "Leaderboard:\n";

        for (int i = 0; i < times.Count; i++)
        {
            int min = Mathf.FloorToInt(times[i] / 60);
            int sec = Mathf.FloorToInt(times[i] % 60);
            displayText += $"{i + 1}. Player - {min:00}:{sec:00}\n";
        }

        leaderboardText.text = displayText;
    }

    void UpdateBestTimeDisplay()
    {
        if (bestTimeText == null) return;

        var times = LoadTimes();
        if (times.Count == 0)
        {
            bestTimeText.text = "Best: --:--";
        }
        else
        {
            float best = times[0];
            int min = Mathf.FloorToInt(best / 60);
            int sec = Mathf.FloorToInt(best % 60);
            bestTimeText.text = $"Best: {min:00}:{sec:00}";
        }
    }

    List<float> LoadTimes()
    {
        var list = new List<float>();
        for (int i = 0; i < MAX_ENTRIES; i++)
        {
            if (PlayerPrefs.HasKey($"Time{i}"))
                list.Add(PlayerPrefs.GetFloat($"Time{i}"));
        }
        list.Sort();
        return list;
    }

    void QuitGame()
    {
        Application.Quit();
    }
    public void ClearLeaderboard()
    {
        for (int i = 0; i < MAX_ENTRIES; i++)
        {
            PlayerPrefs.DeleteKey($"Time{i}");
        }
        PlayerPrefs.Save();

        UpdateLeaderboardDisplay();
        UpdateBestTimeDisplay(); // 同时刷新 BestTime 显示
        Debug.Log("Leaderboard cleared.");
    }
}
