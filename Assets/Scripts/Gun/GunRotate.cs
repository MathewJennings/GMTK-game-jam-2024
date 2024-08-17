using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunRotate : MonoBehaviour
{
    Camera camera;

    [SerializeField]
    private Transform tip;
    [SerializeField]
    private Canvas canvas; 

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
    }
}
