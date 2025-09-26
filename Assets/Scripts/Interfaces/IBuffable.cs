public interface IBuffable : IHasHealth
{
    void Heal(int healAmount);
    void BuffSpeed(float speed);
    void SetShield(int shieldAmount);
    void BuffDamage(int damageAmount);
    void BuffPierce(bool pierce);
    void ResetSpeed();
    void ResetDamage();
    void ResetShield();
    void ResetPierce();
}