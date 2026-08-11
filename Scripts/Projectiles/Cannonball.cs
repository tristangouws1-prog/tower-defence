using UnityEngine;

public class Cannonball : Projectile
{
    public float splashRadius = 2.5f;

    protected override void OnHit()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, splashRadius, LayerMask.GetMask("Enemy"));
        foreach (Collider c in hits)
        {
            Enemy enemy = c.GetComponent<Enemy>();
            if (enemy != null) enemy.TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}
