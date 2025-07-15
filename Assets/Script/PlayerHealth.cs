using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float health = 12f;
    public float maxHealth = 12f;
    public Image healthBar;

    private bool isDead = false;
    public GameManager gameManager;

    void Start()
    {
        maxHealth = health;
    }

    void Update()
    {
        healthBar.fillAmount = Mathf.Clamp01(health / maxHealth);

        if (health <= 0 && !isDead)
        {
            isDead = true;
            Die();
        }
    }

    void Die()
    {
        gameManager.GameOver();

        gameObject.SetActive(false);
    }
}