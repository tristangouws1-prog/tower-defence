using UnityEngine;

public class KnightHero : Hero
{
    [Header("Shield Bash")]
    public float stunDuration = 1.5f;
    public float bashRadius = 3f;

    // Knight: melee range, high HP, special = Shield Bash (stuns nearby enemies)

    protected override void UseSpecial()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, bashRadius, LayerMask.GetMask("Enemy"));
        foreach (Collider c in hits)
        {
            EnemyMovement em = c.GetComponent<EnemyMovement>();
            if (em != null) em.Stun(stunDuration);
            Enemy enemy = c.GetComponent<Enemy>();
            if (enemy != null) enemy.TakeDamage(attackDamage * 0.5f);
        }
    }
}
