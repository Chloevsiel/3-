using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    public GameObject bulletTypeA;
    public GameObject bulletTypeB;
    public Transform shootPoint;
    public float shootInterval = 2f; // 发射间隔秒数

    private float shootTimer = 0f;

    void Update()
    {
        shootTimer -= Time.deltaTime;
        if (shootTimer <= 0f)
        {
            Shoot();
            shootTimer = shootInterval;
        }
    }

    void Shoot()
    {
        if (shootPoint == null)
        {
            Debug.LogWarning("ShootPoint 未设置！");
            return;
        }

        Transform player = GameObject.FindGameObjectWithTag("Player")?.transform;
        if (player == null) return;

        // 计算指向玩家的方向
        Vector2 direction = (player.position - shootPoint.position).normalized;

        // 随机选择子弹预制体
        GameObject selectedBullet = Random.value < 0.5f ? bulletTypeA : bulletTypeB;

        // 实例化子弹
        GameObject bullet = Instantiate(selectedBullet, shootPoint.position, Quaternion.identity);

        // 设置子弹方向
        BulletDamage bulletScript = bullet.GetComponent<BulletDamage>();
        if (bulletScript != null)
        {
            bulletScript.SetDirection(direction);
        }
    }
}
