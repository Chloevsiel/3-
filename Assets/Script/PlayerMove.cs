using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float Horizontal;
    public float speed = 5f;
    public float runSpeed = 8f;
    public KeyCode runKey = KeyCode.LeftShift;
    public float jumpingPower = 12f;
    public float jumpAccelerationFactor = 0.2f; // 控制加速跳的系数
    private bool isFacingRight = true;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] LayerMask groundLayer;

    private Animator animator;
    private float jumpBufferTime = 0.2f;
    private float jumpBufferCounter;

    public AudioSource audioSource;         // 用于播放单次音效（跳跃、跑步）
    public AudioClip runClip;
    public AudioClip jumpClip;

    public AudioClip walkClip;              // ✅ 新增：走路音效
    private AudioSource walkAudioSource;    // ✅ 新增：走路循环用的 AudioSource

    private bool runKeyPressedLastFrame = false;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();

        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();

        // ✅ 初始化用于走路音效的 AudioSource
        walkAudioSource = gameObject.AddComponent<AudioSource>();
        walkAudioSource.clip = walkClip;
        walkAudioSource.loop = true;
        walkAudioSource.playOnAwake = false;
        walkAudioSource.volume = 0.4f; // 可自定义音量
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            jumpBufferCounter = jumpBufferTime;
            PlayJumpSound();
        }
        else if (jumpBufferCounter > 0)
        {
            jumpBufferCounter -= Time.deltaTime;
        }

        HandleRunSound();
        HandleWalkSound(); // ✅ 控制走路音效
    }

    void FixedUpdate()
    {
        Horizontal = Input.GetAxisRaw("Horizontal");
        float currentSpeed = Input.GetKey(runKey) ? runSpeed : speed;

        bool isGrounded = IsGrounded();
        animator.SetBool("isJumping", !isGrounded && rb.velocity.y > 0.1f);
        animator.SetBool("isRunning", Mathf.Abs(Horizontal) > 0.1f && isGrounded);

        if (jumpBufferCounter > 0 && isGrounded)
        {
            float adjustedJumpPower = jumpingPower + (Mathf.Abs(Horizontal) * currentSpeed * jumpAccelerationFactor);
            rb.velocity = new Vector2(rb.velocity.x, adjustedJumpPower);
            jumpBufferCounter = 0;
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        rb.velocity = new Vector2(Horizontal * currentSpeed, rb.velocity.y);

        Flip();
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.35f, groundLayer);
    }

    private void Flip()
    {
        if (isFacingRight && Horizontal < 0f || !isFacingRight && Horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private void PlayJumpSound()
    {
        if (audioSource != null && jumpClip != null)
        {
            audioSource.PlayOneShot(jumpClip, 0.3f); // 0.3f 是跳跃音量
        }
    }

    private void HandleRunSound()
    {
        bool runKeyPressed = Input.GetKey(runKey);
        bool isGrounded = IsGrounded();

        if (runKeyPressed && !runKeyPressedLastFrame && isGrounded)
        {
            Debug.Log("跑步音效触发");
            if (audioSource != null && runClip != null)
            {
                audioSource.PlayOneShot(runClip);
            }
            else
            {
                Debug.LogWarning("AudioSource 或 runClip 未赋值！");
            }
        }

        runKeyPressedLastFrame = runKeyPressed;
    }

    private void HandleWalkSound()
    {
        bool isGrounded = IsGrounded();
        bool isMoving = Mathf.Abs(Horizontal) > 0.1f;

        if (isMoving && isGrounded)
        {
            if (!walkAudioSource.isPlaying && walkClip != null)
            {
                walkAudioSource.Play();
            }
        }
        else
        {
            if (walkAudioSource.isPlaying)
            {
                walkAudioSource.Stop();
            }
        }
    }
}
