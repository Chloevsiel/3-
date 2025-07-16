using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingRock : MonoBehaviour
{
    public string playerTag = "Player";
    public string deadZoneTag = "DeadZone";

    public float damage = 2f;

    private bool isFalling = false;
    private Rigidbody2D rb;
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        // ��ʼʱ��������Ӱ�죬�ȴ���Ҵ���
        if (rb != null)
            rb.isKinematic = true;

        if (animator != null)
            animator.SetBool("isFalling", false);
    }

    void Update()
    {
        if (isFalling && rb != null && rb.isKinematic)
        {
            // ���������˶����ᣬ������ʼ����/����
            rb.isKinematic = false;

            if (animator != null)
                animator.SetBool("isFalling", true);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isFalling && collision.CompareTag(playerTag))
        {
            // ��ҿ�������ʼ��ʯ����
            isFalling = true;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag(deadZoneTag))
        {
            // ����DeadZone�����Լ�
            Destroy(gameObject);
            return;
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("��ʯײ������ˣ�");
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                Debug.Log("׼�����������˺�");
                playerHealth.TakeDamage(damage, transform.position);
            }
            else
            {
                Debug.LogWarning("��������Ҳ���PlayerHealth���");
            }
            Destroy(gameObject);
        }
    }

}