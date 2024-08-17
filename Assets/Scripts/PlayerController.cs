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

    private HashSet<GameObject> groundsBeingTouched = new();
    private bool startedAJump = false;
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
        if (Input.GetKeyDown(KeyCode.Space) &&
            (groundsBeingTouched.Count > 0 || (Time.time - timeLastTouchedGround < coyoteTimeAmount)))
        {
            startedAJump = true;
        }
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

        if (startedAJump)
        {
            rb.AddForce(Vector2.up * jumpMult, ForceMode2D.Impulse);
            // Reset jump state
            startedAJump = false;
            timeLastTouchedGround = -10f;
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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        CheckForGroundCollision(collision.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CheckForGroundCollision(collision.gameObject);
    }

    private void CheckForGroundCollision(GameObject gameObject)
    {
        if (gameObject.GetComponent<JumpableSurface>() != null)
        {
            groundsBeingTouched.Add(gameObject);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        CheckForGroundExit(collision.gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        CheckForGroundExit(collision.gameObject);
    }

    private void CheckForGroundExit(GameObject gameObject)
    {
        if (gameObject.GetComponent<JumpableSurface>() != null)
        {
            groundsBeingTouched.Remove(gameObject);
            if (groundsBeingTouched.Count == 0)
            {
                //dont retrigger coyote time if we just jumped
                if (!Input.GetKey(KeyCode.Space))
                {
                    timeLastTouchedGround = Time.time;
                }
            }
        }
    }
}
