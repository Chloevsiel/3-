using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    public GameObject bulletTypeA;
    public GameObject bulletTypeB;
    public Transform shootPoint;
    public float shootInterval = 2f; // ����������

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
            Debug.LogWarning("ShootPoint δ���ã�");
            return;
        }

        Transform player = GameObject.FindGameObjectWithTag("Player")?.transform;
        if (player == null) return;

        // ����ָ����ҵķ���
        Vector2 direction = (player.position - shootPoint.position).normalized;

        // ���ѡ���ӵ�Ԥ����
        GameObject selectedBullet = Random.value < 0.5f ? bulletTypeA : bulletTypeB;

        // ʵ�����ӵ�
        GameObject bullet = Instantiate(selectedBullet, shootPoint.position, Quaternion.identity);

        // �����ӵ�����
        BulletDamage bulletScript = bullet.GetComponent<BulletDamage>();
        if (bulletScript != null)
        {
            bulletScript.SetDirection(direction);
        }
    }
}
