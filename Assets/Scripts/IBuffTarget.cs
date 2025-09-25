using UnityEngine;

public interface IBuffTarget
{
    void BuffSpeed(float speed);
    void SetShield(int shieldAmount);
    void BuffDamage(int damageAmount);
}
