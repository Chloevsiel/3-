using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool locked = true;
    public GameObject gameWinUI;
    public Timer timer;

    public AudioSource audioSource;         // ✅ 用于播放声音
    public AudioClip openDoorClip;          // ✅ 开门音效

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();

        if (locked && animator != null)
        {
            animator.Play("DoorIdle", 0, 0);
            animator.speed = 0f;
        }

        if (timer == null)
        {
            timer = FindObjectOfType<Timer>();
            if (timer == null)
            {
                Debug.LogError("未在场景中找到 Timer 脚本！");
            }
        }

        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                Debug.LogWarning("没有 AudioSource 组件，将自动添加一个。");
                audioSource = gameObject.AddComponent<AudioSource>();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Key"))
        {
            locked = false;

            if (animator != null)
            {
                animator.speed = 1f;
                animator.SetTrigger("Open");
            }

            // ✅ 播放开门音效
            if (audioSource != null && openDoorClip != null)
            {
                audioSource.PlayOneShot(openDoorClip);
            }
            else
            {
                Debug.LogWarning("开门音效未设置或 AudioSource 缺失！");
            }
        }
    }

    public void OnDoorOpenComplete()
    {
        if (gameWinUI != null)
        {
            gameWinUI.SetActive(true);
        }
        else
        {
            Debug.LogError("gameWinUI 未分配！");
        }

        if (timer != null)
        {
            timer.OnGameWin();
        }
        else
        {
            Debug.LogError("Timer 未分配，无法调用 OnGameWin！");
        }
    }
}
