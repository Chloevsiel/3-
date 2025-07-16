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
                pHealth.TakeDamage(damage); // 调用 TakeDamage 函数来减血

                if (pHealth.IsAlive() && respawn != null) // 你可以在 PlayerHealth 加一个 IsAlive() 公共方法
                {
                    other.transform.position = respawn.lastSafePosition;
                }
                // 如果血量 <= 0，会由 PlayerHealth 脚本自行处理死亡
            }
        }
    }
}