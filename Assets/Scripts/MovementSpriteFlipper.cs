using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocitySpriteFlipper : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer spriteRenderer;

    // Update is called once per frame
    void Update()
    {
        // Don't read inputs if time is frozen (i.e. we are paused).
        if (Time.timeScale <= 0)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            spriteRenderer.flipX = true;
        } else if (Input.GetKeyDown(KeyCode.D))
        {
            spriteRenderer.flipX = false;
        }
    }
}
