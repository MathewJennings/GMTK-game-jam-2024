using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealOrbChase : MonoBehaviour
{
    private Vector3 origin;
    private Vector3 target;
    private float duration = 0.5f;
    private float timeElapsedPercentage = 0.0f;

    private void Start()
    {
        duration = 0.5f;
        timeElapsedPercentage = 0.0f;
    }

    public void SetOrigin(Vector3 o)
    {
        origin = o;
    }
    public void SetTarget(Vector3 t)
    {
        target = t;
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsedPercentage += Time.deltaTime / duration;
        gameObject.transform.position = Vector3.Slerp(origin, target, timeElapsedPercentage);
        if (timeElapsedPercentage >= 1.0f)
        {
            Destroy(gameObject);
        }
    }
}
