using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Timer : MonoBehaviour
{
    [SerializeField] Text timerText;
    [SerializeField] Text bestText;


    private float elapsedTime = 0f;
    private bool isWinFlag = false;
    private const int MAX_ENTRIES = 5;

    void Start()
    {
        ResetTimer();
    }

    void Update()
    {
        if (isWinFlag) return;

        elapsedTime += Time.deltaTime;
        UpdateTimerDisplay(elapsedTime);
    }

    void UpdateTimerDisplay(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        if (timerText != null)
            timerText.text = string.Format("Time: {0:00}:{1:00}", minutes, seconds);
    }

    public void OnGameWin()
    {
        if (isWinFlag) return;

        isWinFlag = true;
        Time.timeScale = 0f;

        SaveCompletionTime(elapsedTime);
        UpdateBestTimeText();

        Debug.Log($"Game Win! Time recorded: {elapsedTime} seconds");
    }

    void SaveCompletionTime(float time)
    {
        var times = LoadTimes();
        times.Add(time);
        times.Sort();

        while (times.Count > MAX_ENTRIES)
            times.RemoveAt(times.Count - 1);

        for (int i = 0; i < times.Count; i++)
        {
            PlayerPrefs.SetFloat($"Time{i}", times[i]);
        }

        PlayerPrefs.Save();
    }

    void UpdateBestTimeText()
    {
        if (bestText == null) return;

        var times = LoadTimes();
        if (times.Count == 0)
        {
            bestText.text = "Best: --:--";
        }
        else
        {
            float best = times[0];
            int min = Mathf.FloorToInt(best / 60);
            int sec = Mathf.FloorToInt(best % 60);
            bestText.text = $"Best: {min:00}:{sec:00}";
        }
    }

    List<float> LoadTimes()
    {
        var list = new System.Collections.Generic.List<float>();
        for (int i = 0; i < MAX_ENTRIES; i++)
        {
            if (PlayerPrefs.HasKey($"Time{i}"))
                list.Add(PlayerPrefs.GetFloat($"Time{i}"));
        }
        list.Sort();
        return list;
    }

    public void ResetTimer()
    {
        elapsedTime = 0f;
        isWinFlag = false;
        UpdateTimerDisplay(elapsedTime);
        UpdateBestTimeText();
    }
}
