using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 baseOffset = new Vector3(0, 1, -10); // ����ƫ������Y���Z��̶���
    public float horizontalOffsetAmount = 3f;  // ���ݳ���X�����ƫ����
    public float smoothSpeed = 5f;

    private Rigidbody2D targetRb2D; // ���ڻ�ȡ��ҳ����ٶȷ���

    void Start()
    {
        if (target != null)
        {
            targetRb2D = target.GetComponent<Rigidbody2D>();
            if (targetRb2D == null)
            {
                Debug.LogWarning("Ŀ�����û�� Rigidbody2D ������޷��жϳ���");
            }
        }
    }

    void Update()
    {
        if (target != null)
        {
            float horizontalOffset = 0f;

            // ��������ٶ��жϳ��򣬸�ǰ������ռ�
            if (targetRb2D != null)
            {
                if (targetRb2D.velocity.x > 0.1f)
                {
                    horizontalOffset = horizontalOffsetAmount; // ����ƫ��
                }
                else if (targetRb2D.velocity.x < -0.1f)
                {
                    horizontalOffset = -horizontalOffsetAmount; // ����ƫ��
                }
            }
            else
            {
                // ���û�� Rigidbody2D��ʹ����ҵľֲ������жϳ��򣨼���x����Ϊ���򸺣�
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