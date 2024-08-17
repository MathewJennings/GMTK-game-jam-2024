using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBehavior : MonoBehaviour
{
    [SerializeField]
    private GameObject sprite;

    [SerializeField]
    private bool scaleFromBottom;

    // Start is called before the first frame update
    void Start()
    {
        if (scaleFromBottom)
        {
            float halfScale = transform.localScale.y / 2;
            transform.position = new Vector3(transform.position.x, transform.position.y - halfScale, transform.position.z);
            sprite.transform.localPosition = new Vector3(0f, 0.5f, 0f);
            transform.GetComponent<BoxCollider2D>().offset = new Vector2(0f, 0.5f);
        }
    }
}
