using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scaler : MonoBehaviour
{

    // This represents how much the GameObject's transform should be scaled
    [SerializeField]
    private float transformScaleMultiplier = 1f;
    [SerializeField]
    private bool scaleTransformX = false;
    [SerializeField]
    private bool scaleTransformY = false;

    void Start()
    {
        UpdateTransformScale();
    }

    void Update()
    {
        
    }

    public float StealTransformScaleMultipler()
    {
        float currentTransformScaleMultiplier = transformScaleMultiplier;
        transformScaleMultiplier = 1f;
        UpdateTransformScale();
        return currentTransformScaleMultiplier;
    }

    public void SetTransformScaleMultiplier(float multiplier)
    {
        transformScaleMultiplier = multiplier;
        UpdateTransformScale();
    }

    private void UpdateTransformScale()
    {
        float newXScale = gameObject.transform.localScale.x;
        float newYScale = gameObject.transform.localScale.y;
        float zScale = gameObject.transform.localScale.z;
        if (scaleTransformX)
        {
            newXScale *= transformScaleMultiplier;
        }
        if (scaleTransformY)
        {
            newYScale *= transformScaleMultiplier;
        }
        gameObject.transform.localScale = new Vector3(newXScale, newYScale, zScale);
    }
}
