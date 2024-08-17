using System.Collections.Generic;
using UnityEngine;

public class PhysicsBasedWaypointFollower : MonoBehaviour
{
    public List<Transform> waypoints;
    public float velocity = 5f;
    public float tolerance = 0.1f;

    private int nextWaypointIndex = 0;

    // Update is called once per frame
    void FixedUpdate()
    {
        // get position info
        if(waypoints.Count == 0) {  return; }
        Vector2 destination = GetNextWaypoint().position;
        Vector2 distance = (Vector2)destination - (Vector2)transform.position;
        float speedScaler = GetComponent<MovementSpeedScaler>().PollMultipler();

        // update body velocity
        Rigidbody2D body = gameObject.GetComponent<Rigidbody2D>();
        body.velocity = distance.normalized * velocity * speedScaler;

        if (distance.magnitude <= tolerance)
        {
            nextWaypointIndex++;
        }
    }

    private Transform GetNextWaypoint()
    {
        return waypoints[nextWaypointIndex % waypoints.Count];
    }
}