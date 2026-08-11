using UnityEngine;

public class MageProjectile : Projectile
{
    public float slowAmount = 0.5f;
    public float slowDuration = 2f;
    public float aoeRadius = 2f;

    protected override void OnHit()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, aoeRadius, LayerMask.GetMask("Enemy"));
        foreach (Collider c in hits)
        {
            Enemy enemy = c.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
                enemy.ApplySlow(slowAmount, slowDuration);
            }
        }
        Destroy(gameObject);
    }
}
