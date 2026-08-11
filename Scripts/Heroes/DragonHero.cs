using System.Collections;
using UnityEngine;

public class DragonHero : Hero
{
    [Header("Fire Breath")]
    public float coneAngle = 60f;
    public GameObject fireBreathEffectPrefab;

    [Header("Flame Surge")]
    public float surgeRadius = 8f;
    public float surgeDuration = 3f;
    public float surgeDamagePerSecond = 30f;

    // Dragon: large radius fire cone attack, special = Flame Surge (full radius burn)

    protected override void Attack(Transform target)
    {
        // Fire cone: damages all enemies in cone toward target
        Vector3 dirToTarget = (target.position - transform.position).normalized;
        Collider[] hits = Physics.OverlapSphere(transform.position, attackRange, LayerMask.GetMask("Enemy"));
        foreach (Collider c in hits)
        {
            Vector3 dirToEnemy = (c.transform.position - transform.position).normalized;
            float angle = Vector3.Angle(dirToTarget, dirToEnemy);
            if (angle <= coneAngle / 2f)
            {
                Enemy enemy = c.GetComponent<Enemy>();
                if (enemy != null) enemy.TakeDamage(attackDamage);
            }
        }
    }

    protected override void UseSpecial()
    {
        StartCoroutine(FlameSurge());
    }

    IEnumerator FlameSurge()
    {
        if (fireBreathEffectPrefab != null)
        {
            GameObject fx = Instantiate(fireBreathEffectPrefab, transform.position, Quaternion.identity);
            Destroy(fx, surgeDuration);
        }

        float elapsed = 0f;
        while (elapsed < surgeDuration)
        {
            Collider[] hits = Physics.OverlapSphere(transform.position, surgeRadius, LayerMask.GetMask("Enemy"));
            foreach (Collider c in hits)
            {
                Enemy enemy = c.GetComponent<Enemy>();
                if (enemy != null) enemy.TakeDamage(surgeDamagePerSecond * Time.deltaTime);
            }
            elapsed += Time.deltaTime;
            yield return null;
        }
    }
}
