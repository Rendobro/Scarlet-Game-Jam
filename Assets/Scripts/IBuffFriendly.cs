using UnityEngine;

public interface IBuffFriendly
{
    void Heal(int healAmount);
    void BuffSpeed(float speed);
    void SetShield(int shieldAmount);
    void BuffDamage(int damageAmount);
}
