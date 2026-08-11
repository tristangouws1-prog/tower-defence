using UnityEngine;

public class CannonTower : Tower
{
    [Header("Cannon Settings")]
    public float splashRadius = 2.5f;

    protected override void Shoot()
    {
        if (projectilePrefab == null || firePoint == null) return;
        GameObject proj = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Cannonball cb = proj.GetComponent<Cannonball>();
        if (cb != null)
        {
            cb.SetTarget(currentTarget, damage);
            cb.splashRadius = splashRadius;
        }
    }
}
