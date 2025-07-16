using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] GameObject player;
    public bool isPickedUp;
    private Vector2 vel;
    public float smoothTime;

    // �����ƵԴ�������
    private AudioSource audioSource;

    // �������Inspector��������Ƶ�����������ô������
    [SerializeField] private AudioClip pickupSound;

    void Start()
    {
        // ��ȡ������ͬһ�������ϵ�AudioSource���
        audioSource = GetComponent<AudioSource>();

        // ���û�й���AudioSource�����Զ����һ��
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
