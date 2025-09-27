using UnityEngine;

public class ProjectileData
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

    public ProjectileData(float speed, float range, int damage, int pierce, bool ricochet, Transform transform, Vector3 direction, GameObject prefab)
    {
        this.speed = speed;
        this.range = range;
        this.damage = damage;
        this.pierce = pierce;
        this.ricochet = ricochet;
        this.transform = transform;
        this.direction = direction;
        this.prefab = prefab;
        Debug.Log("Projectile created with transform: " + transform ?? "null");
        parent = transform.parent;
    }

    public override string ToString()
    {
        return $"ProjectileData(speed: {speed},"+
            $"\nrange: {range}, "+
            $"\ndamage: {damage},"+
            $"\npierce: {pierce},"+
            $"\nricochet: {ricochet},"+
            $"\ndirection: {direction},"+
            $"\nprefab: {prefab?.name ?? "null"})";
    }
}