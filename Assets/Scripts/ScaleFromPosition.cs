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
        Vector2 transformationVector = new Vector2(0f, 0f);
        if (scaleFrom == ScaleFrom.Bottom)
        {
            float halfScale = transform.localScale.y / 2;
            transform.position = new Vector3(transform.position.x, transform.position.y - halfScale, transform.position.z);
            transformationVector = new Vector2(0f, 0.5f);
        }
        if (scaleFrom == ScaleFrom.Top)
        {
            float halfScale = transform.localScale.y / 2;
            transform.position = new Vector3(transform.position.x, transform.position.y + halfScale, transform.position.z);
            transformationVector = new Vector2(0f, -0.5f);
        }
        if (scaleFrom == ScaleFrom.Left)
        {
            float halfScale = transform.localScale.x / 2;
            transform.position = new Vector3(transform.position.x - halfScale, transform.position.y, transform.position.z);
            transformationVector = new Vector2(0.5f, 0f);
        }
        if (scaleFrom == ScaleFrom.Right)
        {
            float halfScale = transform.localScale.x / 2;
            transform.position = new Vector3(transform.position.x + halfScale, transform.position.y, transform.position.z);
            transformationVector = new Vector2(-0.5f, 0f);
        }
        transform.GetComponent<BoxCollider2D>().offset += transformationVector;
        updateChildren(transformationVector);
    }

    private void updateChildren(Vector2 childTransformationVector)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            child.transform.localPosition += new Vector3(childTransformationVector.x, childTransformationVector.y, 0f);
        }
    }
}
