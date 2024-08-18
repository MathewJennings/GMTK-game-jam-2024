using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBodyFlipper : MonoBehaviour
{
    [SerializeField]
    private Transform playerTransform;
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private RectTransform indicatorsCanvas;

    private void Update()
    {
        // Don't read inputs if time is frozen (i.e. we are paused).
        if (Time.timeScale <= 0)
        {
            return;
        }

        // Set rotation of gun.
        Vector3 mousePos = Input.mousePosition;
        // Offset the camera's z position.
        mousePos.z = Camera.main.transform.position.z * -1;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        float xOffset = (mousePos - playerTransform.position).x;

        if (xOffset < 0)
        {
            if (!spriteRenderer.flipY)
            {
                Vector3 currScale = indicatorsCanvas.localScale;
                indicatorsCanvas.localScale = new Vector3(currScale.x, -1 * currScale.y, currScale.z);
            }
            spriteRenderer.flipY = true;
        } else if (xOffset > 0)
        {
            if (spriteRenderer.flipY)
            {
                Vector3 currScale = indicatorsCanvas.localScale;
                indicatorsCanvas.localScale = new Vector3(currScale.x, -1 * currScale.y, currScale.z);
            }
            spriteRenderer.flipY = false;
        }
    }
}
