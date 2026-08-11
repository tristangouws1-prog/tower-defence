using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [Header("Tower Stats")]
    public float range = 5f;
    public float damage = 25f;
    public float fireRate = 1f;
    public int cost = 100;

    [Header("Prefabs")]
    public GameObject projectilePrefab;
    public Transform firePoint;

    protected float fireTimer;
    protected Transform currentTarget;

    protected virtual void Update()
    {
        FindTarget();
        if (currentTarget != null)
        {
            RotateToTarget();
            fireTimer += Time.deltaTime;
            if (fireTimer >= 1f / fireRate)
            {
                fireTimer = 0f;
                Shoot();
            }
        }
    }

    protected virtual void FindTarget()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, range, LayerMask.GetMask("Enemy"));
        float furthestProgress = -1f;
        currentTarget = null;

        foreach (Collider hit in hits)
        {
            EnemyMovement em = hit.GetComponent<EnemyMovement>();
            if (em != null)
            {
                // Target enemy furthest along the path
                float progress = em.PathProgress;
                if (progress > furthestProgress)
                {
                    furthestProgress = progress;
                    currentTarget = hit.transform;
                }
            }
        }
    }

    protected virtual void RotateToTarget()
    {
        Vector3 dir = currentTarget.position - transform.position;
        dir.y = 0;
        if (dir != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(dir);
    }

    protected virtual void Shoot()
    {
        if (projectilePrefab == null || firePoint == null) return;
        GameObject proj = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Projectile p = proj.GetComponent<Projectile>();
        if (p != null) p.SetTarget(currentTarget, damage);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
