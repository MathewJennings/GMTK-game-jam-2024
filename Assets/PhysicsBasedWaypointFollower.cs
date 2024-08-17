using System.Collections.Generic;
using UnityEngine;

public class PhysicsBasedWaypointFollower : MonoBehaviour
{
    public List<Transform> waypoints;
    public float velocity = 5f;

    private int nextWaypointIndex = 0;

    // Update is called once per frame
    void FixedUpdate()
    {
        if(waypoints.Count == 0) {  return; }
        Vector2 destination = GetNextWaypoint().position;
        Vector2 distance = (Vector2)destination - (Vector2)transform.position;
        float speedScaler = GetComponent<MovementSpeedScaler>().PollMultipler();
        gameObject.GetComponent<Rigidbody2D>().velocity = distance.normalized * velocity * speedScaler; 
        if (distance.magnitude <= 0.1)
        {
            nextWaypointIndex++;
        }
    }

    private Transform GetNextWaypoint()
    {
        return waypoints[nextWaypointIndex % waypoints.Count];
    }
}