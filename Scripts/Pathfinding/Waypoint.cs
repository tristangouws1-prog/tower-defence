using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [Tooltip("Next waypoints — add more than one to create a branch")]
    public List<Waypoint> nextWaypoints = new List<Waypoint>();

    public bool IsEndPoint => nextWaypoints.Count == 0;

    public Waypoint GetNextWaypoint()
    {
        if (nextWaypoints.Count == 0) return null;
        return nextWaypoints[Random.Range(0, nextWaypoints.Count)];
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, 0.3f);
        foreach (Waypoint next in nextWaypoints)
        {
            if (next != null)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawLine(transform.position, next.transform.position);
            }
        }
    }
}
