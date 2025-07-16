using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragileGround : MonoBehaviour
{
    public float delayBeforeDestroy = 0.5f;  // ����ȥ������ʧ
    public float respawnDelay = 1f;          // ��ʧ���ø�ԭ

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
        // �������鶯��
        animator.SetBool("isBreaking", true);

        yield return new WaitForSeconds(delayBeforeDestroy);

        // ������ײ�����أ�����Ӧ���������Ч����
        col.enabled = false;
        spriteRenderer.enabled = false;

        // �ȴ���ԭʱ��
        yield return new WaitForSeconds(respawnDelay);

        // ���ö�����״̬
        animator.SetBool("isBreaking", false);
        col.enabled = true;
        spriteRenderer.enabled = true;

        isTriggered = false;
    }
}