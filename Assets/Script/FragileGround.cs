using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragileGround : MonoBehaviour
{
    public float delayBeforeDestroy = 0.5f;  // ����ȥ������ʧ
    public float respawnDelay = 1f;          // ��ʧ���ø�ԭ

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

        // ��ƽ̨����ʧ�������ﲻ��Destroy�����ǽ�����ײ������
        col.enabled = false;
        spriteRenderer.enabled = false;

        // �ȴ���ԭʱ��
        yield return new WaitForSeconds(respawnDelay);

        // ƽ̨��ԭ
        col.enabled = true;
        spriteRenderer.enabled = true;

        isTriggered = false;
    }
}
