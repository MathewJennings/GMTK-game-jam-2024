using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float moveMult = 10;
    [SerializeField]
    private MovementSpeedScaler speedScalar;
    [SerializeField]
    private GameObject feet;

    //[SerializeField]
    //private float gravityScale = 10;
    //[SerializeField]
    //private float fallingGravityScale = 40;

    // Start is called before the first frame update
    void Start()
    {
        
    }


    private void FixedUpdate()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        // Player movement
        float speedScalerMultiplier = speedScalar.PollMultipler();
        // Only read movement input if we'll have a non-zero movement speed.
        if (moveMult > 0 && speedScalerMultiplier > 0)
        {
            Vector2 currentYVelocity = gameObject.GetComponent<Rigidbody2D>().velocity * Vector2.up;
            if (Input.GetKey(KeyCode.A))
            {
                rb.velocity = currentYVelocity + (Vector2.left * moveMult * speedScalerMultiplier);
            }

            if (Input.GetKey(KeyCode.D))
            {
                rb.velocity = currentYVelocity + (Vector2.right * moveMult * speedScalerMultiplier);
            }
        }

    }
}
