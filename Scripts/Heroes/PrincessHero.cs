using UnityEngine;

public class PrincessHero : Hero
{
    [Header("Arcane Nova")]
    public float novaRadius = 5f;
    public float novaSlowAmount = 0.6f;
    public float novaSlowDuration = 3f;
    public GameObject novaEffectPrefab;

    // Princess: ranged magic, special = Arcane Nova (AoE damage + slow)

    protected override void Attack(Transform target)
    {
        Enemy enemy = target.GetComponent<Enemy>();
        if (enemy != null) enemy.TakeDamage(attackDamage);
    }

    protected override void UseSpecial()
    {
        if (novaEffectPrefab != null)
        {
            GameObject fx = Instantiate(novaEffectPrefab, transform.position, Quaternion.identity);
            Destroy(fx, 2f);
        }

        Collider[] hits = Physics.OverlapSphere(transform.position, novaRadius, LayerMask.GetMask("Enemy"));
        foreach (Collider c in hits)
        {
            Enemy enemy = c.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(attackDamage * 1.5f);
                enemy.ApplySlow(novaSlowAmount, novaSlowDuration);
            }
        }
    }
}
