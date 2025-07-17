using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    public GameObject gameOverUI;
    public GameObject gameWinUI;

    private AudioSource bgmSource;

    void Start()
    {
        bgmSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // �������һ�� UI �������ֹͣ BGM ����
        if ((gameOverUI != null && gameOverUI.activeInHierarchy) ||
            (gameWinUI != null && gameWinUI.activeInHierarchy))
        {
            if (bgmSource != null && bgmSource.isPlaying)
            {
                bgmSource.Stop();
            }
        }
    }
}
