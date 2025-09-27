public interface IBuffable : IHasHealth
{
    void BuffSpeed(float speed);
    void SetShield(int shieldAmount);
    void BuffDamage(int damageAmount);
    void BuffPierce(int pierce);
    void ResetSpeed();
    void ResetDamage();
    void ResetShield();
    void ResetPierce();
}