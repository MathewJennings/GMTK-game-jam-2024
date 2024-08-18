using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{

    [SerializeField]
    private Vector3 rotationSpeed;

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }
}
