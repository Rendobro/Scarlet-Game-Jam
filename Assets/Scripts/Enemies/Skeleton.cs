using UnityEngine;
[System.Serializable]
public class Skeleton : Enemy
{
    private float range = 4.5f;
    private int _health = 8;
    public override int MaxHealth { get; protected set; } = 8;
    public override int Damage { get; protected set; } = 3;
    public override float Speed { get; protected set; } = 4f;
    public override int Health
    {
        get => _health;
        protected set
        {
            if (value <= 0) Perish();
            else _health = value;
        }
    }
    public override void Attack(IHasHealth target)
    {
        Vector3 targetPos = (target as MonoBehaviour).transform.position;
        if ((transform.position - targetPos).magnitude <= range)
        {
            target.TakeDamage(Damage);
        }
        else
        {
            Debug.Log($"Target {(target as MonoBehaviour).gameObject.name} out of range");
        }
    }
    public override void ResetSpeed() => Speed = 4f;
    public override void ResetDamage() => Damage = 3;
    
}