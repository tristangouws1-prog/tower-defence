using UnityEngine;

public class Projectile : MonoBehaviour
{
    protected Transform target;
    protected float damage;
    public float speed = 12f;

    public virtual void SetTarget(Transform t, float dmg)
    {
        target = t;
        damage = dmg;
    }

    protected virtual void Update()
    {
        if (target == null) { Destroy(gameObject); return; }
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        transform.LookAt(target);
        if (Vector3.Distance(transform.position, target.position) < 0.2f) OnHit();
    }

    protected virtual void OnHit()
    {
        Enemy enemy = target.GetComponent<Enemy>();
        if (enemy != null) enemy.TakeDamage(damage);
        Destroy(gameObject);
    }
}
