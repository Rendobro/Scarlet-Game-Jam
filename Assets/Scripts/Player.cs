using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Player : MonoBehaviour, IHasHealth, IBuffTarget
{
    // using this "Instance" is a common way to implement 
    // the singleton pattern in Unity, you can google it
    public static Player Instance { get; private set; }
    public static event System.Action OnPlayerDeath;
    int health = 5;
    int maxHealth = 5;
    int shield = 0;
    int speed = 5;
    int damage = 1;

    List<Buff> playerBuffs = Buff.initializeBuffs();

    // Awake is called before Start, but not after a scene is loaded, so only use if 
    // you are able to initialize something before the scene is loaded
    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;
    }


    // Note: in C# if theres no "public" or "private" for methods like start and update it 
    // just means it defaults to private
    void Start()
    {
        
    }
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        int overflowDamage = Mathf.Max(damage - shield, 0);
        shield = Mathf.Max(shield - damage, 0);
        health = Mathf.Max(health-overflowDamage,0);
        
        if (health == 0) OnPlayerDeath?.Invoke();
    }

    public void Heal(int amount)
    {
        health = Mathf.Min(health + amount, maxHealth);
    }

    public int GetHealth()
    {
        return health;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }

    public void BuffSpeed(float speed)
    {
        speed += speed;
    }

    public void SetShield(int shieldAmount)
    {
        shield = shieldAmount;
    }

    public void BuffDamage(int damageAmount)
    {
        damage += damageAmount;
    }
}
