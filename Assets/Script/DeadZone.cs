using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    public float damage = 2f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth pHealth = other.GetComponent<PlayerHealth>();
            PlayerRespawn respawn = other.GetComponent<PlayerRespawn>();

            if (pHealth != null)
            {
                pHealth.TakeDamage(damage); // ���� TakeDamage ��������Ѫ

                if (pHealth.IsAlive() && respawn != null) // ������� PlayerHealth ��һ�� IsAlive() ��������
                {
                    other.transform.position = respawn.lastSafePosition;
                }
                // ���Ѫ�� <= 0������ PlayerHealth �ű����д�������
            }
        }
    }
}