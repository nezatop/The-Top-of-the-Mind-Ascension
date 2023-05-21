using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    public static Action StartFight;
    public static Action EnemyDie;
    public static Action EnemyAtack;

    [Range(0, 10)]
    [SerializeField] private int StartingHealth = 1;

    private int Health;

    private void Awake()
    {
        Health = StartingHealth;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartFight?.Invoke();
            FightMenu.OnChoseRightAnswer += RightAnswer;
            FightMenu.OnChoseNotRightAnswer += NotRightAnswer;
            Inventory.UseDamageItem += RightAnswer;
        }
    }

    public void RightAnswer()
    {
        Health = Mathf.Clamp(Health - 1, 0, StartingHealth);

        if (Health <= 0)
        {
            Destroy(gameObject);
        }

    }
    public void NotRightAnswer()
    {
        EnemyAtack?.Invoke();
    }

    private void OnDestroy()
    {
        EnemyDie?.Invoke();
        FightMenu.OnChoseRightAnswer -= RightAnswer;
        FightMenu.OnChoseNotRightAnswer -= NotRightAnswer;
        Inventory.UseDamageItem -= RightAnswer;
    }
}
