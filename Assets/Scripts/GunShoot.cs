using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShoot : MonoBehaviour
{
    Camera camera;

    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        // Offset the camera's z position.
        mousePos.z = camera.transform.position.z * -1;
        mousePos = camera.ScreenToWorldPoint(mousePos);
        Debug.DrawRay(transform.position, mousePos - transform.position, Color.green);

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, mousePos - transform.position);
            if (hit) {
                Scaler hitScaler = hit.transform.gameObject.GetComponent<Scaler>();
                Scaler meScaler = gameObject.transform.parent.gameObject.GetComponentInParent<Scaler>();
                hitScaler.SwapTransformScaleMultiplier(meScaler);
            }
        }
    }
}
