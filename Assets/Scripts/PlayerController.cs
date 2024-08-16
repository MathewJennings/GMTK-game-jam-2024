using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float moveMult = 10;
    [SerializeField]
    private float jumpMult = 50;
    //[SerializeField]
    //private float gravityScale = 10;
    //[SerializeField]
    //private float fallingGravityScale = 40;

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
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        // Player movement
        Vector2 currentYVelocity = gameObject.GetComponent<Rigidbody2D>().velocity * Vector2.up;
        if (Input.GetKey(KeyCode.A))
        {
            rb.velocity = currentYVelocity + (Vector2.left * moveMult);
        }

        if (Input.GetKey(KeyCode.D))
        {
            rb.velocity = currentYVelocity + (Vector2.right * moveMult);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            if (rb.velocity.y == 0)
            {
                rb.AddForce(Vector2.up * jumpMult, ForceMode2D.Impulse);
            }
        }

        //// Scale gravity based on jumping or falling.
        //if (rb.velocity.y >= 0)
        //{
        //    rb.gravityScale = gravityScale;
        //}
        //else if (rb.velocity.y < 0)
        //{
        //    rb.gravityScale = fallingGravityScale;
        //}
    }
}
