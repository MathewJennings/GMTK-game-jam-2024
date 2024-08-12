using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movable : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Debug.Log("left key was pressed");
            gameObject.transform.position += Vector3.left;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Debug.Log("right key was pressed");
            gameObject.transform.position += Vector3.right;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Debug.Log("up key was pressed");
            gameObject.transform.position += Vector3.up;
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Debug.Log("down key was pressed");
            gameObject.transform.position += Vector3.down;
        }
    }
}
