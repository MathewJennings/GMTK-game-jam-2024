using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBodyFlipper : MonoBehaviour
{
    [SerializeField]
    private Transform playerTransform;
    [SerializeField]
    private SpriteRenderer spriteRenderer;

    private void Update()
    {
        // Set rotation of gun.
        Vector3 mousePos = Input.mousePosition;
        // Offset the camera's z position.
        mousePos.z = Camera.main.transform.position.z * -1;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        float xOffset = (mousePos - playerTransform.position).x;

        if (xOffset < 0)
        {
            spriteRenderer.flipY = true; 
        } else if (xOffset > 0)
        {
            spriteRenderer.flipY = false;
        }
    }
}
