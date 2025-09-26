using System;
using System.Collections.Generic;
using UnityEngine;
public class Player : MonoBehaviour, IHasHealth, IBuffFriendly
{
    // using this "Instance" is a common way to implement 
    // the singleton pattern in Unity, you can google it
    public static Player Instance { get; private set; }
    public static event Action OnPlayerDeath;
    private bool aoeEnabled = false;
    private bool lifestealEnabled = false;
    private bool pierceEnabled = false;
    private bool ricochetEnabled = false;
    private bool enemyDebuffEnabled = false;
    private bool regenEnabled = false;
    private bool healFlag = false;
#pragma warning disable IDE0044 // Add readonly modifier
    private float healInterval = 8f;
#pragma warning restore IDE0044 // Add readonly modifier
    [SerializeField] private int health = 5;
#pragma warning disable IDE0044 // Add readonly modifier
    [SerializeField] private int maxHealth = 5;
#pragma warning restore IDE0044 // Add readonly modifier
    [SerializeField] private int shield = 0;
    [SerializeField] private float speed = 5;
    [SerializeField] private int damage = 1;
    [SerializeField] private int counter = 0;
    Timer timer = new Timer(8f);
#pragma warning disable IDE0044 // Add readonly modifier
    List<Buff> playerBuffs = Buff.initializeBuffs();
#pragma warning restore IDE0044 // Add readonly modifier

    // Awake is called before Start, but not after a scene is loaded, so only use if 
    // you are able to initialize something before the scene is loaded
    private void Awake()
    {
        timer = new Timer(healInterval);
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;
    }


    // Note: in C# if theres no "public" or "private" for methods like start and update it 
    // just means it defaults to private
    void Start()
    {

        timer.Start();
        foreach (Buff buff in playerBuffs)
        {
            Debug.Log("Activated buff: " + buff.buffType);
            buff.Activate();
        }
    }
    void Update()
    {
        timer.Update();
        if (timer.IsFinished)
        {
            timer.Start();
            healFlag = true;
        }
        if (regenEnabled && health < maxHealth && healFlag)
        {
            healFlag = false;
            Heal(1);
            Debug.Log("Player healed 1 health. Current health: " + health);
        }
    }

    // Self explanatory enabling and disable buffs
    // However, only use Buff objects and Activate() !!!
    public void EnableAOE() => aoeEnabled = true;
    public void DisableAOE() => aoeEnabled = false;
    public void EnableLifesteal() => lifestealEnabled = true;
    public void DisableLifesteal() => lifestealEnabled = false;
    public void EnablePierce() => pierceEnabled = true;
    public void DisablePierce() => pierceEnabled = false;
    public void EnableRicochet() => ricochetEnabled = true;
    public void DisableRicochet() => ricochetEnabled = false;
    public void EnableEnemyDebuff() => enemyDebuffEnabled = true;
    public void DisableEnemyDebuff() => enemyDebuffEnabled = false;
    public void EnableRegen() => regenEnabled = true;
    public void DisableRegen() => regenEnabled = false;
    public void TakeDamage(int damage)
    {
        int overflowDamage = Mathf.Max(damage - shield, 0);
        shield = Mathf.Max(shield - damage, 0);
        health = Mathf.Max(health - overflowDamage, 0);

        if (health <= 0) OnPlayerDeath?.Invoke();
    }
    public void Attack(IHasHealth target)
    {
        target.TakeDamage(damage);
        counter++;
        if (lifestealEnabled && counter >= 5)
        {
            counter = 0;
            Heal(1);
        }
    }
    public void Heal(int amount) => health = Mathf.Min(health + amount, maxHealth);
    public int GetHealth() => health;
    public int GetMaxHealth() => maxHealth;
    public void BuffSpeed(float speed) => this.speed += speed;
    public void SetShield(int shieldAmount) => shield = shieldAmount;
    public void BuffDamage(int damageAmount) => damage += damageAmount;
    public void BuffPierce(bool pierce) => pierceEnabled = pierce;
    public void ResetSpeed() => speed = 5;
    public void ResetDamage() => damage = 1;
    public void ResetShield() => shield = 0;
    public void ResetPierce() => pierceEnabled = false;
}