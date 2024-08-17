using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMovement : MonoBehaviour
{
    [SerializeField]
    private Transform followTarget;
    [SerializeField]
    private Vector3 offsetFromFollowTarget = new Vector3(0,0,50);

    // Update is called once per frame
    void Update()
    {
        transform.position = followTarget.transform.position + offsetFromFollowTarget;
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        renderer.material.mainTextureOffset += new Vector2(1 * Time.deltaTime, 0);
        Debug.Log(renderer.material.mainTextureOffset);
    }
}
