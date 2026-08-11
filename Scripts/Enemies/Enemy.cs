using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [Header("Stats")]
    public float maxHp = 100f;
    public float speed = 3f;
    public int goldReward = 10;
    public int liveDamage = 1;

    float currentHp;
    float baseSpeed;

    [Header("UI")]
    public Slider hpBar;

    protected EnemyMovement movement;

    protected virtual void Awake()
    {
        currentHp = maxHp;
        baseSpeed = speed;
        movement = GetComponent<EnemyMovement>();
    }

    public virtual void TakeDamage(float amount)
    {
        currentHp -= amount;
        if (hpBar != null) hpBar.value = currentHp / maxHp;
        if (currentHp <= 0) Die();
    }

    public void ApplySlow(float amount, float duration)
    {
        StopCoroutine(nameof(SlowCoroutine));
        StartCoroutine(SlowCoroutine(amount, duration));
    }

    IEnumerator SlowCoroutine(float amount, float duration)
    {
        speed = baseSpeed * (1f - amount);
        yield return new WaitForSeconds(duration);
        speed = baseSpeed;
    }

    protected virtual void Die()
    {
        GameManager.Instance?.EarnGold(goldReward);
        WaveManager.Instance?.OnEnemyDied();
        Destroy(gameObject);
    }

    public void ReachedBase()
    {
        GameManager.Instance?.LoseLife(liveDamage);
        WaveManager.Instance?.OnEnemyDied();
        Destroy(gameObject);
    }
}
