using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] GameObject player;
    public bool isPickedUp;
    private Vector2 vel;
    public float smoothTime;

    // 添加音频源组件引用
    private AudioSource audioSource;

    // 你可以在Inspector里拖入音频剪辑，或者用代码加载
    [SerializeField] private AudioClip pickupSound;

    void Start()
    {
        // 获取挂载在同一个物体上的AudioSource组件
        audioSource = GetComponent<AudioSource>();

        // 如果没有挂载AudioSource，则自动添加一个
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Update()
    {
        if (isPickedUp)
        {
            Vector3 offset = new Vector3(0, 1, 0);
            transform.position = Vector2.SmoothDamp(transform.position, player.transform.position + offset, ref vel, smoothTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !isPickedUp)
        {
            isPickedUp = true;
            PlayPickupSound();
        }
    }

    private void PlayPickupSound()
    {
        if (audioSource != null && pickupSound != null)
        {
            audioSource.PlayOneShot(pickupSound);
        }
        else
        {
            Debug.LogWarning("AudioSource or pickupSound is missing!");
        }
    }
}
