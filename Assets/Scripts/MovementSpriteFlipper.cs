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
        if (Input.GetKeyDown(KeyCode.A))
        {
            spriteRenderer.flipX = true;
        } else if (Input.GetKeyDown(KeyCode.D))
        {
            spriteRenderer.flipX = false;
        }
    }
}
