using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealOrbChase : MonoBehaviour
{
    private Transform origin;
    private Transform target;
    private float duration = 0.5f;
    private float timeElapsedPercentage = 0.0f;

    private void Start()
    {
        duration = 0.5f;
        timeElapsedPercentage = 0.0f;
    }

    public void SetOrigin(Transform o)
    {
        origin = o;
    }
    public void SetTarget(Transform t)
    {
        target = t;
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsedPercentage += Time.deltaTime / duration;
        if (origin && target)
        {
            gameObject.transform.position = Vector3.Slerp(origin.position, target.position, timeElapsedPercentage);
        }
        if (timeElapsedPercentage >= 1.0f)
        {
            Destroy(gameObject);
        }
    }
}
