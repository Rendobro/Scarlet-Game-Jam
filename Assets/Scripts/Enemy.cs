public abstract class Enemy : MonoBehaviour, IHasHealth
{
    public static event System.Action OnEnemyDeath;

    // Make sure if you want to implement a shield 
    // so damage doesnt immediately go through health
    // to do that on your own otherwise you really 
    // do not need to
    public abstract void TakeDamage(int damage);
    public void Heal(int amount) => health = Mathf.Min(health + amount, maxHealth);
    public int GetHealth() => health;
    public int GetMaxHealth() => maxHealth;

    public void Died()
    {
        if (health <= 0)
        {
            OnEnemyDeath?.Invoke();
            Destroy(this);
        }
    }
    protected int health { get; set; } = 20;
    protected int maxHealth { get; set; } = 20;
    protected int damage { get; set; } = 1;
    protected float speed { get; set; } = 1f;
}