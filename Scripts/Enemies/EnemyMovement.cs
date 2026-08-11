using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    Waypoint currentTarget;
    Enemy enemy;
    PathBlocker currentBlocker;
    bool isAttackingBlocker = false;
    float attackTimer = 0f;
    float pathProgress = 0f;

    [Header("Attack Blocker Settings")]
    public float attackRate = 1f;
    public float attackDamage = 10f;

    public float PathProgress => pathProgress;

    void Start()
    {
        enemy = GetComponent<Enemy>();
        Waypoint[] allWaypoints = FindObjectsByType<Waypoint>(FindObjectsSortMode.None);
        float closest = float.MaxValue;
        foreach (Waypoint wp in allWaypoints)
        {
            float dist = Vector3.Distance(transform.position, wp.transform.position);
            if (dist < closest)
            {
                closest = dist;
                currentTarget = wp;
            }
        }
    }

    void Update()
    {
        if (GameManager.Instance.IsGameOver) return;
        if (isAttackingBlocker) { AttackBlocker(); return; }
        MoveToWaypoint();
    }

    void MoveToWaypoint()
    {
        if (currentTarget == null) return;
        Vector3 dir = (currentTarget.transform.position - transform.position).normalized;
        transform.position += dir * enemy.speed * Time.deltaTime;
        transform.LookAt(new Vector3(currentTarget.transform.position.x, transform.position.y, currentTarget.transform.position.z));
        pathProgress += enemy.speed * Time.deltaTime;

        if (Vector3.Distance(transform.position, currentTarget.transform.position) < 0.2f)
        {
            if (currentTarget.IsEndPoint) { enemy.ReachedBase(); return; }
            currentTarget = currentTarget.GetNextWaypoint();
        }
    }

    void AttackBlocker()
    {
        if (currentBlocker == null) { isAttackingBlocker = false; return; }
        attackTimer += Time.deltaTime;
        if (attackTimer >= attackRate)
        {
            attackTimer = 0f;
            currentBlocker.TakeDamage(attackDamage);
        }
    }

    public void BlockedBy(PathBlocker blocker)
    {
        currentBlocker = blocker;
        isAttackingBlocker = true;
        attackTimer = 0f;
    }

    public void BlockerDestroyed()
    {
        currentBlocker = null;
        isAttackingBlocker = false;
    }

    public void Stun(float duration)
    {
        StartCoroutine(StunCoroutine(duration));
    }

    System.Collections.IEnumerator StunCoroutine(float duration)
    {
        float savedSpeed = enemy.speed;
        enemy.speed = 0f;
        isAttackingBlocker = false;
        yield return new WaitForSeconds(duration);
        enemy.speed = savedSpeed;
    }
}
