public class Goblin : Enemy
{

    protected int shield = 10; // Goblin has a shield that takes damage first
    set health = 30, maxHealth = 30, damage = 2, speed = 0.5f;

    public override void TakeDamage(int damage)
    {
        if (shield <= 0)
        {
            health -= damage;
        }
        else
        {
            shield -= damage;
        }
    }
}