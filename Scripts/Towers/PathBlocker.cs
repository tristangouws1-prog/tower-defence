using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Base class for anything placed ON the path (barriers and heroes)
public class PathBlocker : MonoBehaviour
{
    [Header("Blocker Stats")]
    public float maxHp = 300f;
    public Slider hpBar;

    protected float currentHp;
    List<EnemyMovement> blockedEnemies = new List<EnemyMovement>();

    protected virtual void Start()
    {
        currentHp = maxHp;
    }

    public virtual void TakeDamage(float amount)
    {
        currentHp -= amount;
        if (hpBar != null) hpBar.value = currentHp / maxHp;
        if (currentHp <= 0) DestroyBlocker();
    }

    protected virtual void DestroyBlocker()
    {
        foreach (EnemyMovement em in blockedEnemies)
            if (em != null) em.BlockerDestroyed();
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        EnemyMovement em = other.GetComponent<EnemyMovement>();
        if (em != null)
        {
            blockedEnemies.Add(em);
            em.BlockedBy(this);
        }
    }

    void OnTriggerExit(Collider other)
    {
        EnemyMovement em = other.GetComponent<EnemyMovement>();
        if (em != null) blockedEnemies.Remove(em);
    }
}
