public class ProjectileData
{
    public GameObject prefab;
    public Transform parent;
    private float speed;
    private float range;
    private int damage;
    private bool pierce;
    private bool ricochet;
    private Transform transform;
    public Transform parent;

    public void Projectile(float speed, float range, int damage, bool pierce, bool ricochet, Transform transform)
    {
        this.speed = speed;
        this.range = range;
        this.damage = damage;
        this.pierce = pierce;
        this.ricochet = ricochet;
        this.transform = transform;
        parent = transform.parent;
    }
}