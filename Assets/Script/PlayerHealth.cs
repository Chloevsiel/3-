using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    private float health;
    public float maxHealth = 12f;
    public Image healthBar;

    public GameManager gameManager;

    private bool isDead = false;
    private bool isInvincible = false;

    [Header("Flash")]
    public Image screenFlash;
    public SpriteRenderer playerSprite;
    public Image healthBarImage;

    [Header("�޵����������")]
    public float invincibleTime = 1f;
    public float knockbackForce = 5f;
    public float knockbackDuration = 0.1f;

    private Rigidbody2D rb;
    private PlayerMove playerMove;

    [Header("��Ч")]
    public AudioSource audioSource;
    public AudioClip hurtClip;

    void Start()
    {
        health = maxHealth;
        rb = GetComponent<Rigidbody2D>();
        playerMove = GetComponent<PlayerMove>();

        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (healthBar != null)
            healthBar.fillAmount = Mathf.Clamp01(health / maxHealth);

        if (health <= 0 && !isDead)
        {
            isDead = true;
            Die();
        }
    }

    public void TakeDamage(float amount)
    {
        TakeDamage(amount, null);
    }

    public void TakeDamage(float amount, Vector2? hitSource)
    {
        if (isDead || isInvincible)
            return;

        health -= amount;

        // ����������Ч
        PlayHurtSound();

        StartCoroutine(InvincibilityFlash());
        StartCoroutine(ScreenFlashRed());

        if (hitSource.HasValue)
        {
            Vector2 knockDir = ((Vector2)transform.position - hitSource.Value).normalized;
            StartCoroutine(ApplyKnockback(knockDir));
        }

        if (health <= 0 && !isDead)
        {
            isDead = true;
            Die();
        }
    }

    void PlayHurtSound()
    {
        if (audioSource != null && hurtClip != null)
        {
            audioSource.PlayOneShot(hurtClip);
        }
    }

    public bool IsAlive()
    {
        return health > 0;
    }

    void Die()
    {
        if (gameManager != null)
            gameManager.gameOver();

        gameObject.SetActive(false);
    }

    IEnumerator InvincibilityFlash()
    {
        isInvincible = true;

        int flashTimes = 4;
        float delay = invincibleTime / (flashTimes * 2);

        for (int i = 0; i < flashTimes; i++)
        {
            if (playerSprite != null)
                playerSprite.color = new Color(1, 1, 1, 0.3f);
            if (healthBarImage != null)
                healthBarImage.color = new Color(1, 1, 1, 0.3f);

            yield return new WaitForSeconds(delay);

            if (playerSprite != null)
                playerSprite.color = Color.white;
            if (healthBarImage != null)
                healthBarImage.color = Color.white;

            yield return new WaitForSeconds(delay);
        }

        isInvincible = false;
    }

    IEnumerator ScreenFlashRed()
    {
        if (screenFlash == null)
            yield break;

        screenFlash.color = new Color(1, 0, 0, 0.5f);
        yield return new WaitForSeconds(0.1f);
        screenFlash.color = new Color(1, 0, 0, 0f);
    }

    IEnumerator ApplyKnockback(Vector2 dir)
    {
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
            rb.AddForce(dir * knockbackForce, ForceMode2D.Impulse);
        }

        if (playerMove != null)
            playerMove.enabled = false;

        yield return new WaitForSeconds(knockbackDuration);

        if (playerMove != null)
            playerMove.enabled = true;
    }
}