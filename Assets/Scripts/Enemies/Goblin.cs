using UnityEngine;
[System.Serializable]
public class Goblin : Enemy
{
    private int shield = 10; 
    private int _health = 30;
    public override int MaxHealth { get; protected set; } = 30;
    public override int Damage { get; protected set; } = 2;
    public override float Speed { get; protected set; } = 0.5f;
    public override int Health
    {
        get => _health;
        protected set
        {
            if (value <= 0) Perish();
            else health = value;
        }
    }

    public override void TakeDamage(int damage)
    {
        int overflowDamage = Mathf.Max(Damage - shield, 0);
        shield = Mathf.Max(shield - Damage, 0);
        Health = Mathf.Max(Health - overflowDamage, 0);
    }
}