using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragileGround : MonoBehaviour
{
    public float delayBeforeDestroy = 0.5f;  // 踩上去后多久消失
    public float respawnDelay = 1f;          // 消失后多久复原

    private Collider2D col;
    private SpriteRenderer spriteRenderer;

    private bool isTriggered = false;

    private void Awake()
    {
        col = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
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
        yield return new WaitForSeconds(delayBeforeDestroy);

        // 让平台“消失”，这里不是Destroy，而是禁用碰撞和隐藏
        col.enabled = false;
        spriteRenderer.enabled = false;

        // 等待复原时间
        yield return new WaitForSeconds(respawnDelay);

        // 平台复原
        col.enabled = true;
        spriteRenderer.enabled = true;

        isTriggered = false;
    }
}
