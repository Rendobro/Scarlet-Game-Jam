using UnityEngine;
public abstract class Enemy : MonoBehaviour, IHasHealth
{
    public static event System.Action OnEnemyDeath;

    // Make sure if you want to implement a shield 
    // so damage doesnt immediately go through health
    // to do that on your own otherwise you really 
    // do not need to
    public abstract void TakeDamage(int damage);
    public void Heal(int amount) => Health = Mathf.Min(Health + amount, MaxHealth);
    public int GetHealth() => Health;
    public int GetMaxHealth() => MaxHealth;
    public virtual void Attack(IHasHealth target)
    {
        target.TakeDamage(Damage);
    }
    public void Follow(IHasHealth target)
    {
        Vector3 targetPos = (target as MonoBehaviour).transform.position;
        Vector3 direction = (targetPos - transform.position).normalized;
        transform.position += direction * Speed * Time.deltaTime;
    }

    public void Perish()
    {
        OnEnemyDeath?.Invoke();
        Debug.Log("Enemy has died. Name: " + gameObject.name);
        Destroy(gameObject);
    }
    [SerializeField] protected int health = 20;
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

}