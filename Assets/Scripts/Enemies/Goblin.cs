using UnityEngine;
[System.Serializable]
public class Goblin : Enemy
{
    private int _shield = 10; 
    private int _health = 30;
    public override int Shield 
    { 
        get => _shield;
        protected set
        {
            _shield = Mathf.Max(0,value);
        }
    }
    public override int MaxHealth { get; protected set; } = 30;
    public override int Damage { get; protected set; } = 2;
    public override float Speed { get; protected set; } = 0.5f;
    public override int Health
    {
        get => _health;
        protected set
        {
            if (value <= 0) Perish();
            else _health = value;
        }
    }
    public override void ResetShield() => Shield = 10;
    public override void ResetSpeed() => Speed = 0.5f;
    public override void ResetDamage() => Damage = 2;
    public void RemoveShield() => Shield = 0;
}