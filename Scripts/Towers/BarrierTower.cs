using UnityEngine;

public class BarrierTower : PathBlocker
{
    [Header("Barrier Settings")]
    public int cost = 75;

    // Barrier is purely defensive — no attack, just blocks enemies
    // Inherits TakeDamage and blocker logic from PathBlocker
}
