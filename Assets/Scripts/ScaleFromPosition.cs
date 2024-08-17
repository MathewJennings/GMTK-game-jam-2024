using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleFromPosition : MonoBehaviour
{
    [SerializeField]
    private GameObject sprite;

    private enum ScaleFrom
    {
        Center,
        Bottom,
        Top,
        Left,
        Right,
    }
    [SerializeField]
    private ScaleFrom scaleFrom;

    // Start is called before the first frame update
    void Start()
    {
        if (scaleFrom == ScaleFrom.Bottom)
        {
            float halfScale = transform.localScale.y / 2;
            transform.position = new Vector3(transform.position.x, transform.position.y - halfScale, transform.position.z);
            sprite.transform.localPosition = new Vector3(0f, 0.5f, 0f);
            transform.GetComponent<BoxCollider2D>().offset = new Vector2(0f, 0.5f);
        }
        if (scaleFrom == ScaleFrom.Top)
        {
            float halfScale = transform.localScale.y / 2;
            transform.position = new Vector3(transform.position.x, transform.position.y + halfScale, transform.position.z);
            sprite.transform.localPosition = new Vector3(0f, -0.5f, 0f);
            transform.GetComponent<BoxCollider2D>().offset = new Vector2(0f, -0.5f);
        }
        if (scaleFrom == ScaleFrom.Left)
        {
            float halfScale = transform.localScale.x / 2;
            transform.position = new Vector3(transform.position.x - halfScale, transform.position.y, transform.position.z);
            sprite.transform.localPosition = new Vector3(0.5f, 0f, 0f);
            transform.GetComponent<BoxCollider2D>().offset = new Vector2(0.5f, 0f);
        }
        if (scaleFrom == ScaleFrom.Right)
        {
            float halfScale = transform.localScale.x / 2;
            transform.position = new Vector3(transform.position.x + halfScale, transform.position.y, transform.position.z);
            sprite.transform.localPosition = new Vector3(-0.5f, 0f, 0f);
            transform.GetComponent<BoxCollider2D>().offset = new Vector2(-0.5f, 0f);
        }
    }
}
