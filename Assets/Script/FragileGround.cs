using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragileGround : MonoBehaviour
{
    public float delayBeforeDestroy = 0.5f;  // 踩上去后多久消失
    public float respawnDelay = 1f;          // 消失后多久复原

    private Collider2D col;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    private bool isTriggered = false;

    private void Awake()
    {
        col = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isTriggered && collision.collider.CompareTag("Player"))
        {
            isTriggered = true;
            StartCoroutine(BreakAndRespawn());
        }
    }

    private IEnumerator BreakAndRespawn()
    {
        // 触发破碎动画
        animator.SetBool("isBreaking", true);

        yield return new WaitForSeconds(delayBeforeDestroy);

        // 禁用碰撞和隐藏（动画应已完成破碎效果）
        col.enabled = false;
        spriteRenderer.enabled = false;

        // 等待复原时间
        yield return new WaitForSeconds(respawnDelay);

        // 重置动画和状态
        animator.SetBool("isBreaking", false);
        col.enabled = true;
        spriteRenderer.enabled = true;

        isTriggered = false;
    }
}