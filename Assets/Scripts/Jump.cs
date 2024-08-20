using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    [SerializeField]
    GameObject jumper;
    [SerializeField]
    private float jumpMult = 50;

    public AudioSource jumpAudioSource;

    private HashSet<GameObject> groundsBeingTouched = new();
    private bool startedAJump = false;
    private float timeLastTouchedGround = -10f;
    private float coyoteTimeAmount = .2f;

    public bool IsPlayerOnGround()
    {
        return groundsBeingTouched.Count > 0;
    }
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

    // Update is called once per frame
    void FixedUpdate()
    {
        Rigidbody2D rb = jumper.GetComponent<Rigidbody2D>();

        if (startedAJump)
        {
            PlayRandomizedJumpSound();
            rb.AddForce(Vector2.up * jumpMult, ForceMode2D.Impulse);
            // Reset jump state
            startedAJump = false;
            timeLastTouchedGround = -10f;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("jump collision enter");
        CheckForGroundCollision(collision.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("jump trigger enter");
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
    private void PlayRandomizedJumpSound()
    {
        jumpAudioSource.pitch = Random.Range(0.6f, 1f);
        jumpAudioSource.volume = Random.Range(0.6f, 1f);
        jumpAudioSource.PlayOneShot(jumpAudioSource.clip);
    }
}
