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

        // 开始时不受物理影响，等待玩家触发
        if (rb != null)
            rb.isKinematic = true;

        if (animator != null)
            animator.SetBool("isFalling", false);
    }

    void Update()
    {
        if (isFalling && rb != null && rb.isKinematic)
        {
            // 解除刚体的运动冻结，让它开始滚动/下落
            rb.isKinematic = false;

            if (animator != null)
                animator.SetBool("isFalling", true);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isFalling && collision.CompareTag(playerTag))
        {
            // 玩家靠近，开始落石下落
            isFalling = true;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag(deadZoneTag))
        {
            // 碰到DeadZone销毁自己
            Destroy(gameObject);
            return;
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("滚石撞到玩家了！");
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                Debug.Log("准备对玩家造成伤害");
                playerHealth.TakeDamage(damage, transform.position);
            }
            else
            {
                Debug.LogWarning("玩家身上找不到PlayerHealth组件");
            }
            Destroy(gameObject);
        }
    }

}