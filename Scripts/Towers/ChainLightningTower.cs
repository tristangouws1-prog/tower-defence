using System.Collections.Generic;
using UnityEngine;

public class ChainLightningTower : Tower
{
    [Header("Chain Settings")]
    public int maxChains = 4;
    public float chainRange = 4f;
    public float damageFalloff = 0.7f;
    public GameObject lightningEffectPrefab;

    protected override void Shoot()
    {
        if (currentTarget == null) return;

        List<Transform> hit = new List<Transform>();
        Transform target = currentTarget;
        float currentDamage = damage;

        for (int i = 0; i < maxChains; i++)
        {
            if (target == null) break;

            Enemy enemy = target.GetComponent<Enemy>();
            if (enemy != null) enemy.TakeDamage(currentDamage);

            SpawnLightningEffect(i == 0 ? firePoint.position : hit[i - 1].position, target.position);
            hit.Add(target);

            target = FindNextChainTarget(target, hit);
            currentDamage *= damageFalloff;
        }
    }

    Transform FindNextChainTarget(Transform from, List<Transform> already)
    {
        Collider[] nearby = Physics.OverlapSphere(from.position, chainRange, LayerMask.GetMask("Enemy"));
        float closest = float.MaxValue;
        Transform next = null;

        foreach (Collider c in nearby)
        {
            if (already.Contains(c.transform)) continue;
            float d = Vector3.Distance(from.position, c.transform.position);
            if (d < closest) { closest = d; next = c.transform; }
        }
        return next;
    }

    void SpawnLightningEffect(Vector3 from, Vector3 to)
    {
        if (lightningEffectPrefab == null) return;
        Vector3 mid = (from + to) / 2f;
        GameObject fx = Instantiate(lightningEffectPrefab, mid, Quaternion.identity);
        Destroy(fx, 0.2f);
    }
}
