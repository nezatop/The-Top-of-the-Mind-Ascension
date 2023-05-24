using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float startingHealth = 3.0f;

    public float currentHealth { get; private set; }

    public bool isDead { get; private set; }

    public static Action OnPlayerDead;
    public static Action OnPlayerHurt;

    private void Awake()
    {
        currentHealth = startingHealth;
        isDead = false;
        DeadZone.EnterDeadZone += Die;
        MenuController.OnRestart += Recreatin;
        Enemy.EnemyAtack += TakeDamage;
    }

    private void OnDisable()
    {
        MenuController.OnRestart -= Recreatin;
        Enemy.EnemyAtack -= TakeDamage;
        DeadZone.EnterDeadZone -= Die;
    }

    public void TakeDamage()
    {
        TakeDamage(1);
    }

    public void Die()
    {
        TakeDamage(startingHealth);
    }

    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if (currentHealth > 0)
        {
            OnPlayerHurt?.Invoke();
        }
        else
        {
            if (!isDead)
            {
                isDead = true;
                OnPlayerDead?.Invoke();
            }
        }
    }

    public void Recreatin()
    {
        currentHealth = startingHealth;
        isDead = false;
    }

    public bool MaxHP()
    {
        return currentHealth == startingHealth ? true : false;
    }

    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }
}