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
    [SerializeField]
    private MovementSpeedScaler speedScalar;

    private bool isTouchingGround = false;
    private float timeLastTouchedGround = -10f;
    private float coyoteTimeAmount = .2f;

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
            rb.velocity = currentYVelocity + (Vector2.left * moveMult * speedScalar.PollMultipler());
        }

        if (Input.GetKey(KeyCode.D))
        {
            rb.velocity = currentYVelocity + (Vector2.right * moveMult * speedScalar.PollMultipler());
        }

        if (Input.GetKey(KeyCode.Space))
        {
            if (isTouchingGround || (Time.time - timeLastTouchedGround < coyoteTimeAmount))
            {
                isTouchingGround = false;
                timeLastTouchedGround = -10f;
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
    private void OnCollisionExit2D(Collision2D collision)
    {
        isTouchingGround = false;
        if (collision.gameObject.tag.Equals("Platform") &&
            //dont retrigger coyote time if we just jumped
            !Input.GetKey(KeyCode.Space))
        {
            timeLastTouchedGround = Time.time;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Platform"))
        {
            isTouchingGround = true;
        }
    }
}
