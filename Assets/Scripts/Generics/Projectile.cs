using UnityEngine;

public class Projectile
{
    public GameObject prefab;
    public Transform parent;
    public Vector3 direction;
    public float speed;
    public float range;
    public int damage;
    public int pierce;
    public bool ricochet;
    public Transform transform;

    public Projectile(float speed, float range, int damage, int pierce, bool ricochet, Transform transform, Vector3 direction, GameObject prefab)
    {
        this.speed = speed;
        this.range = range;
        this.damage = damage;
        this.pierce = pierce;
        this.ricochet = ricochet;
        this.transform = transform;
        this.direction = direction;
        parent = transform.parent;
    }
}