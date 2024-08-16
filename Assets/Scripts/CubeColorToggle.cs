using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeColorToggle : MonoBehaviour
{

    public Material cubeMaterial;
    private Color original;


    private void Start()
    {
        original = cubeMaterial.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            cubeMaterial.color = Color.red;
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            cubeMaterial.color = Color.green;
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            cubeMaterial.color = Color.blue;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            cubeMaterial.color = original;
        }


    }
}
