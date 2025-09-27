using System;
using System.Collections.Generic;
using UnityEngine;
public class Player : MonoBehaviour, IHasHealth, IBuffable
{
    // using this "Instance" is a common way to implement 
    // the singleton pattern in Unity, you can google it
    public static Player Instance { get; private set; }
    [SerializeField] private Transform _t;
    // Assign this in the editor
    [SerializeField] private GameObject bulletPrefab;
    private ProjectileData proj;
    public static event Action OnPlayerDeath;
    private bool aoeEnabled = false;
    private bool lifestealEnabled = false;
    private bool ricochetEnabled = false;
    private bool enemyDebuffEnabled = false;
    private bool regenEnabled = false;
    private bool healFlag = false;
    private float healInterval = 8f;
    private float shootCooldown = 0.5f;
    private float aoeAbilityCooldown = 10f;
    [SerializeField] private int health = 5;
    [SerializeField] private int maxHealth = 5;
    [SerializeField] private int shield = 0;
    [SerializeField] private float speed = 5;
    [SerializeField] private int damage = 1;
    [SerializeField] private int pierce = 1;
    [SerializeField] private int counter = 0;
    private Timer healTimer;
    private Timer shootTimer;
    private Timer invincibilityTimer;
    private Timer aoeAbilityTimer;
    public List<Buff> activeBuffs { get; } = Buff.initializeBuffs();

    // Awake is called before Start, but not after a scene is loaded, so only use if 
    // you are able to initialize something before the scene is loaded
    private void Awake()
    {
        healTimer = new Timer(healInterval);
        shootTimer = new Timer(shootCooldown);
        aoeAbilityTimer = new Timer(aoeAbilityCooldown);
        invincibilityTimer = new Timer(0.4f);
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;
    }

    // Note: in C# if theres no "public" or "private" for methods like start and update it 
    // just means it defaults to private
    void Start()
    {

        healTimer.Start();
        shootTimer.Start();
        invincibilityTimer.Start();
        aoeAbilityTimer.Start();    
        foreach (Buff buff in activeBuffs)
        {
            buff.Activate();
        }
    }
    void Update()
    {
        HealTimerUpdater();
        ShootTimerUpdater();
        InvincibilityTimerUpdater();
        RegenChecker();
        MoveDetector();
        ShootDetector();
        AbilityChecker();
        DeathChecker();
    }
    public Transform getTransform() => _t.parent;
    private void DeathChecker()
    {
        if (health <= 0)
        {
            OnPlayerDeath?.Invoke();
            Debug.Log("Player has died.");
            Destroy(gameObject);
        }
    }
    private void AbilityChecker()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Buff aoeBuff = activeBuffs.Find(b => b.buffType == Buff.BuffType.AOEAbility);
            if (aoeBuff.IsEnabled())
            {
                aoeBuff.Activate();
            }
            else
            {
                aoeBuff.Deactivate();
            }
        }
    }

    private void MoveDetector()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveX, moveY);
        Camera.main.transform.position += movement * speed * Time.deltaTime;
    }
    private void ShootDetector()
    {
        if (Input.GetButton("Fire1") && shootTimer.IsFinished)
        {
            shootTimer.Start();
            Vector3 mousePos = Input.mousePosition - new Vector3(Screen.width / 2, Screen.height / 2);
            mousePos.z = 0;
            Vector3 dir = (mousePos - _t.position).normalized;

            // Send projectile in direction until it collides with an enemy, wall, or screen edge, or runs out of range
            proj = new(6f, 10f, damage, pierce, ricochetEnabled, _t, dir, bulletPrefab);
            ProjectileManager.Instance.ShootProjectile(proj);
        }
    }
    private void HealTimerUpdater()
    {
        if (healTimer == null)
        {
            Debug.LogError("healTimer is not initialized!");
            return;
        }
        healTimer.Update();
        if (healTimer.IsFinished && !healFlag) healFlag = true;

    }
    private void ShootTimerUpdater()
    {
        if (shootTimer == null)
        {
            Debug.LogError("shootTimer is not initialized!");
            return;
        }
        shootTimer.Update();
    }
    private void InvincibilityTimerUpdater()
    {
        if (invincibilityTimer == null)
        {
            Debug.LogError("invincibilityTimer is not initialized!");
            return;
        }
        invincibilityTimer.Update();
    }
    private void RegenChecker()
    {
        if (regenEnabled && health < maxHealth && healFlag)
        {
            healFlag = false;
            Heal(1);
            Debug.Log("Player healed 1 health. Current health: " + health);
        }
    }

    public ProjectileData GetProjectileData() => proj;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && enemyDebuffEnabled)
        {
            if (collision.TryGetComponent(out Enemy enemy) && invincibilityTimer.IsFinished)
            {
                invincibilityTimer.Start();
                enemy.BuffDamage(-1);
                TakeDamage(enemy.Damage);
                Debug.Log($"Enemy {enemy.gameObject.name} attacked! Health: {health}");
            }
        }
        else if (collision.CompareTag("Enemy"))
        {
            if (collision.TryGetComponent(out Enemy enemy) && invincibilityTimer.IsFinished)
            {
                invincibilityTimer.Start();
                TakeDamage(enemy.Damage);
                Debug.Log($"Enemy {enemy.gameObject.name} attacked! Health: {health}");
            }
        }
    }
    // Self explanatory enabling and disable buffs
    // However, only use Buff objects and Activate() !!!
    public bool IsAOEEnabled() => aoeEnabled;
    public void EnableAOE() => aoeEnabled = true;
    public void DisableAOE() => aoeEnabled = false;
    public bool IsLifestealEnabled() => lifestealEnabled;
    public void EnableLifesteal() => lifestealEnabled = true;
    public void DisableLifesteal() => lifestealEnabled = false;
    public bool IsRicochetEnabled() => ricochetEnabled;
    public void EnableRicochet() => ricochetEnabled = true;
    public void DisableRicochet() => ricochetEnabled = false;
    public bool IsEnemyDebuffEnabled() => enemyDebuffEnabled;
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
    public void BuffPierce(int pierce) => pierce += pierce;
    public void ResetSpeed() => speed = 5;
    public void ResetDamage() => damage = 1;
    public void ResetShield() => shield = 0;
    public void ResetPierce() => pierce = 1;

}