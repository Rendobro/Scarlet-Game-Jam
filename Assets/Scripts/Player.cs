using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Player : MonoBehaviour, IHasHealth, IBuffTarget
{
    // using this "Instance" is a common way to implement 
    // the singleton pattern in Unity, you can google it
    public static Player Instance { get; private set; }
    public static event System.Action OnPlayerDeath;
    private boolean aoeEnabled = true;
    private boolean lifestealEnabled = true;
    private boolean pierceEnabled = true;
    private boolean ricochetEnabled = true;
    private boolean enemyDebuffEnabled = true;
    private boolean regenEnabled = true;
    private boolean healFlag = true;
    private float healInterval = 8f;
    private int health = 5;
    private int maxHealth = 5;
    private int shield = 0;
    private int speed = 5;
    private int damage = 1;

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
        Timer timer = new Timer(healInterval);
        timer.Start();
        foreach (Buff buff in playerBuffs)
        {
            buff.Activate();
            Console.WriteLine("Activated buff: " + buff.buffType);
        }
    }
    void Update()
    {
        timer.Update();
        if (regenEnabled && health < maxHealth && healFlag)
        {
            healFlag = false;
            Heal(1);
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

        if (health == 0) OnPlayerDeath?.Invoke();
    }
    public void Attack(IHasHealth target)
    {
        target.TakeDamage(damage);
        if (lifestealEnabled && counter % 5 = 0)
        {
            Heal(1);
        }
    }
    public void Heal(int amount) => health = Mathf.Min(health + amount, maxHealth);
    public int GetHealth() => health;
    public int GetMaxHealth() => maxHealth;
    public void BuffSpeed(float speed) => this.speed += speed; 
    public void SetShield(int shieldAmount) => shield = shieldAmount;
    public void BuffDamage(int damageAmount) => damage += damageAmount;
}
