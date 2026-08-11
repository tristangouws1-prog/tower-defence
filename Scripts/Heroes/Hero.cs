using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Heroes are placed on the path like barriers, but actively fight back
public class Hero : PathBlocker
{
    [Header("Hero Stats")]
    public float attackRange = 4f;
    public float attackDamage = 40f;
    public float attackRate = 1f;
    public int cost = 150;

    [Header("Special Ability")]
    public float specialCooldown = 10f;

    [Header("Levelling")]
    public int level = 1;
    public int killsToNextLevel = 10;
    int kills = 0;

    protected float attackTimer;
    protected float specialTimer;
    protected bool specialReady = false;

    protected override void Start()
    {
        base.Start();
        specialTimer = specialCooldown;
    }

    protected virtual void Update()
    {
        if (GameManager.Instance.IsGameOver) return;

        attackTimer += Time.deltaTime;
        specialTimer += Time.deltaTime;

        if (specialTimer >= specialCooldown) specialReady = true;

        Transform target = FindClosestEnemy();
        if (target != null && attackTimer >= 1f / attackRate)
        {
            attackTimer = 0f;
            Attack(target);
        }

        if (specialReady)
        {
            specialTimer = 0f;
            specialReady = false;
            UseSpecial();
        }
    }

    protected Transform FindClosestEnemy()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, attackRange, LayerMask.GetMask("Enemy"));
        Transform closest = null;
        float minDist = float.MaxValue;
        foreach (Collider c in hits)
        {
            float d = Vector3.Distance(transform.position, c.transform.position);
            if (d < minDist) { minDist = d; closest = c.transform; }
        }
        return closest;
    }

    protected virtual void Attack(Transform target)
    {
        Enemy enemy = target.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(attackDamage);
            OnKill();
        }
    }

    protected virtual void UseSpecial() { }

    void OnKill()
    {
        kills++;
        if (kills >= killsToNextLevel * level) LevelUp();
    }

    void LevelUp()
    {
        level++;
        attackDamage *= 1.15f;
        maxHp *= 1.1f;
        currentHp = maxHp;
        if (hpBar != null) hpBar.value = 1f;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
