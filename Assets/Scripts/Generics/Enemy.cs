using System.Collections.Generic;
using UnityEngine;
public abstract class Enemy : MonoBehaviour, IHasHealth, IBuffable
{
    public static event System.Action OnEnemyDeath;
    // Buff related methods
    public virtual void BuffSpeed(float speedAmount) => Speed += speedAmount;
    public virtual void SetShield(int shieldAmount) => Shield += shieldAmount;
    public virtual void BuffDamage(int damageAmount) => Damage += damageAmount;
    public virtual void BuffPierce(int pierceAmount) => Pierce += pierceAmount;

    // Buff storage
    public List<Buff> activeBuffs { get; } = Buff.initializeBuffs();

    // set these to their default values in the class
    public virtual void ResetSpeed() => Speed = 0.7f;
    public virtual void ResetDamage() => Damage = 1;
    public virtual void ResetShield() => Shield = 0;
    public virtual void ResetPierce() => Pierce = 1;

    // Health related methods
    public virtual void TakeDamage(int damage)
    {
        int overflowDamage = Mathf.Max(Damage - Shield, 0);
        Shield = Mathf.Max(Shield - Damage, 0);
        Health = Mathf.Max(Health - overflowDamage, 0);
    }
    public void Heal(int amount) => Health = Mathf.Min(Health + amount, MaxHealth);
    public int GetHealth() => Health;
    public int GetMaxHealth() => MaxHealth;

    // Enemy actions
    public virtual void Attack(IHasHealth target)
    {
        target.TakeDamage(Damage);
    }
    public void Follow(Transform target)
    {
        Vector3 targetPos = target.transform.position;
        Vector3 direction = (targetPos - transform.position).normalized;
        Debug.DrawRay(transform.position, direction * 5, Color.red);
        transform.position += direction * Speed * Time.deltaTime;
    }
    public void Perish()
    {
        OnEnemyDeath?.Invoke();
        Debug.Log("Enemy has died. Name: " + gameObject.name);
        Destroy(gameObject);
    }

    [SerializeField] protected int health = 20;

    // Enemy properties
    public virtual int Health
    {
        get => health;
        protected set
        {
            if (value <= 0) Perish();
            else health = value;
        }
    }
    [SerializeField] protected int maxHealth = 20;
    public virtual int MaxHealth { get => maxHealth; protected set => maxHealth = value; }

    [SerializeField] protected int damage = 1;
    public virtual int Damage { get => damage; protected set => damage = value; }

    [SerializeField] protected float speed = 1f;
    public virtual float Speed { get => speed; protected set => speed = value; }

    [SerializeField] protected int shield = 0;
    public virtual int Shield 
    { 
        get => shield; 
        protected set => shield = (int)Mathf.Max(0,value);
        
    }
    [SerializeField] protected int pierce = 1;
    public virtual int Pierce { get => pierce; protected set => pierce = (int)Mathf.Max(1,value); }


}