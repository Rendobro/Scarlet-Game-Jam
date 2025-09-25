using UnityEngine;

public interface IHasHealth
{
    void TakeDamage(int damage);
    void Heal(int amount);
    int GetHealth();
    int GetMaxHealth();
}
