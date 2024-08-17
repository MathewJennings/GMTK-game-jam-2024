using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GunRotate : MonoBehaviour
{
    Camera camera;

    [SerializeField]
    private Transform player;
    [SerializeField]
    private Transform tip;
    [SerializeField]
    private Canvas canvas;
    [SerializeField]
    private TMP_Text text;

    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        // Set rotation of gun.
        Vector3 mousePos = Input.mousePosition;
        // Offset the camera's z position.
        mousePos.z = camera.transform.position.z * -1;
        mousePos = camera.ScreenToWorldPoint(mousePos);
        transform.right = mousePos - transform.position;

        // Position the canvas above the tip of the gun and rotate it rightside up.
        canvas.transform.position = tip.transform.position + new Vector3(0f, 0.5f, 0f);
        canvas.transform.rotation = camera.transform.rotation;

        // Set the text for the canvas
        GunShoot.Mode currentMode = tip.GetComponent<GunShoot>().GetMode();
        if (canvas.transform.childCount == 1) // Sanity check there's one child of canvas, which we expect is text.
        {
            if (currentMode == GunShoot.Mode.SizeScale)
            {
                SizeScaler sizeScaler = player.GetComponent<SizeScaler>();
                float sizeMult = sizeScaler.GetTransformScaleMultiplier();
                text.SetText("Size\n" + sizeMult + "x");
            }
            else if (currentMode == GunShoot.Mode.MovementSpeedScale)
            {
                MovementSpeedScaler movementSpeedScaler = player.GetComponent<MovementSpeedScaler>();
                float speedMult = movementSpeedScaler.getMultiplier();
                text.SetText("Speed\n" + speedMult + "x");
            }
        }
    }
}
