using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scaler : MonoBehaviour
{

    // This represents how much the GameObject's transform should be scaled
    [SerializeField]
    private float transformScaleMultiplier = 1f;

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
        gameObject.transform.localScale *= transformScaleMultiplier;
    }
}
