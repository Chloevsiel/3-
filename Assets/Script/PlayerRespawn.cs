using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    public Vector3 lastSafePosition;
    private float groundedTime = 0f;
    public float groundedThreshold = 0.6f;  // 玩家要站稳多久才更新安全点

    private bool isGrounded = false;

    void Start()
    {
        lastSafePosition = transform.position;
    }

    void Update()
    {
        if (isGrounded)
        {
            groundedTime += Time.deltaTime;
            if (groundedTime >= groundedThreshold)
            {
                lastSafePosition = transform.position;
            }
        }
        else
        {
            groundedTime = 0f;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
