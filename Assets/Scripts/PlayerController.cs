using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float moveMult = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.A))
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.left * moveMult;
            //gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.left * moveMult);
        }

        if (Input.GetKey(KeyCode.D))
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.right * moveMult;
            //gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.right * moveMult);
        }

        //if (Input.GetKeyDown(KeyCode.UpArrow))
        //{
        //    Debug.Log("up key was pressed");
        //    gameObject.transform.position += Vector3.up;
        //}

        //if (Input.GetKeyDown(KeyCode.DownArrow))
        //{
        //    Debug.Log("down key was pressed");
        //    gameObject.transform.position += Vector3.down;
        //}
    }
}
