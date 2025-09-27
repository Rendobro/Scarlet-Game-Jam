using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private ProjectileData projectileData;
    [SerializeField] private Rigidbody2D rb;
    void Update()
    {
        projectileData = Player.Instance.GetProjectileData();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"Projectile collided with {collision.gameObject.name}");
        if (collision.CompareTag("Enemy"))
        {
            if (collision.TryGetComponent(out Enemy enemy))
            {
                Debug.Log("Projectile hit an enemy!" + enemy.name);
                enemy.TakeDamage(projectileData.damage);
                Debug.Log($"Enemy health & shield after hit: {enemy.GetHealth()} & {enemy.Shield}");
                projectileData.pierce--;
                if (projectileData.pierce <= 0)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}
