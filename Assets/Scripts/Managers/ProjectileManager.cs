using UnityEngine;
using System.Collections;
public class ProjectileManager : MonoBehaviour
{
    public static ProjectileManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this.gameObject);
        else
            Instance = this;
    }

    // Needs to be updated
    public GameObject ShootProjectile(ProjectileData projectile)
    {
        Transform parentT = projectile.parent.transform;
        GameObject newProjectile = Instantiate(projectile.prefab, parentT.position, Quaternion.LookRotation(projectile.direction.normalized, Vector3.up) * Quaternion.Euler(0,90,90));
        newProjectile.transform.SetParent(null);
        Vector3 targetPos = newProjectile.transform.position + projectile.direction * projectile.range;
        StartCoroutine(MoveObjLinear(newProjectile, newProjectile.transform.position, targetPos, projectile.range / projectile.speed));
        return newProjectile;
    }

    private IEnumerator MoveObjLinear(GameObject obj, Vector3 startPos, Vector3 endPos, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration && obj != null)
        {
            float t = elapsed / duration;
            obj.transform.position = Vector3.Lerp(startPos, endPos, t);
            elapsed += Time.deltaTime;
            yield return null;
        }
        if (obj == null) yield break;
        obj.transform.position = endPos;
        Destroy(obj);
        yield break;
    }
}