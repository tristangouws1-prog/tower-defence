using UnityEngine;

public class MageTower : Tower
{
    [Header("Mage Settings")]
    public float slowAmount = 0.5f;
    public float slowDuration = 2f;
    public float aoeRadius = 2f;

    protected override void Shoot()
    {
        if (projectilePrefab == null || firePoint == null) return;
        GameObject proj = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        MageProjectile mp = proj.GetComponent<MageProjectile>();
        if (mp != null)
        {
            mp.SetTarget(currentTarget, damage);
            mp.slowAmount = slowAmount;
            mp.slowDuration = slowDuration;
            mp.aoeRadius = aoeRadius;
        }
    }
}
