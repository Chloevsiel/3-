using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 baseOffset = new Vector3(0, 1, -10); // 基础偏移量（Y轴和Z轴固定）
    public float horizontalOffsetAmount = 3f;  // 根据朝向X轴额外偏移量
    public float smoothSpeed = 5f;

    private Rigidbody2D targetRb2D; // 用于获取玩家朝向（速度方向）

    void Start()
    {
        if (target != null)
        {
            targetRb2D = target.GetComponent<Rigidbody2D>();
            if (targetRb2D == null)
            {
                Debug.LogWarning("目标对象没有 Rigidbody2D 组件，无法判断朝向");
            }
        }
    }

    void Update()
    {
        if (target != null)
        {
            float horizontalOffset = 0f;

            // 根据玩家速度判断朝向，给前方更多空间
            if (targetRb2D != null)
            {
                if (targetRb2D.velocity.x > 0.1f)
                {
                    horizontalOffset = horizontalOffsetAmount; // 朝右偏移
                }
                else if (targetRb2D.velocity.x < -0.1f)
                {
                    horizontalOffset = -horizontalOffsetAmount; // 朝左偏移
                }
            }
            else
            {
                // 如果没有 Rigidbody2D，使用玩家的局部缩放判断朝向（假设x缩放为正或负）
                if (target.localScale.x > 0)
                {
                    horizontalOffset = horizontalOffsetAmount;
                }
                else
                {
                    horizontalOffset = -horizontalOffsetAmount;
                }
            }

            Vector3 desiredPosition = target.position + baseOffset + new Vector3(horizontalOffset, 0, 0);
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
            transform.position = smoothedPosition;
        }
    }
}