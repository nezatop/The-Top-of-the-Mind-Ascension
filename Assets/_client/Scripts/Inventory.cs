using UnityEngine;
using System;
using TMPro;

public class Inventory : MonoBehaviour
{
    #region Render
    [Header("Render")]
    [SerializeField] private TextMeshProUGUI HeailngItemsCounter;
    [SerializeField] private TextMeshProUGUI DamageItemsCounter;
    #endregion

    private GameObject Player;

    #region MaxValues
    [Header("MaxValues")]
    [SerializeField] private int MaxHealingItemsCount;
    [SerializeField] private int MaxDamageItemsCount;
    #endregion

    private int _healingItemsCount = 0;
    private int _damageItemsCount = 0;

    private bool _canUseDI = false;

    public static Action UseDamageItem;

    private void OnEnable()
    {
        Player = GameObject.FindGameObjectWithTag("Player");

        Enemy.EnemyDie += GainBonus;
        MenuController.OnFight += CanUseDI;
        MenuController.OnPlayed += CantUseDI;
    }

    private void OnDisable()
    {
        Enemy.EnemyDie -= GainBonus;
        MenuController.OnFight -= CantUseDI;
        MenuController.OnPlayed -= CantUseDI;
    }

    void CanUseDI()
    {
        _canUseDI= true;
    }
    void CantUseDI()
    {
        _canUseDI= false;
    }

    private void Update()
    {
        HeailngItemsCounter.text = _healingItemsCount.ToString();
        DamageItemsCounter.text = _damageItemsCount.ToString();

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            AddHealingItems(5);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            AddDamageItems(5);
        }
        if(Input.GetKeyDown(KeyCode.E))
        {
            UseHealingItems();
        }
        if (Input.GetKeyDown(KeyCode.Q) && _canUseDI)
        {
            UseDamageItems();
        }
    }

    public void GainBonus()
    {
        AddHealingItems(UnityEngine.Random.Range(0,3));
        AddDamageItems(UnityEngine.Random.Range(0, 3));
    }

    public void AddHealingItems(int healingItemsCount)
    {
        _healingItemsCount = Mathf.Clamp(_healingItemsCount + healingItemsCount, 0, MaxHealingItemsCount);
    }
    public void AddDamageItems(int damageItemsCount)
    {
        _damageItemsCount = Mathf.Clamp(_damageItemsCount + damageItemsCount, 0, MaxDamageItemsCount);
    }

    public void UseHealingItems()
    {
        if (_healingItemsCount > 0 && !Player.GetComponent<Health>().MaxHP())
        {
            _healingItemsCount--;
            Player.GetComponent<Health>().AddHealth(1);
        }
    }
    public void UseDamageItems()
    {
        if (_damageItemsCount > 0)
        {
            _damageItemsCount--;
            UseDamageItem?.Invoke();
        }
    }
}
