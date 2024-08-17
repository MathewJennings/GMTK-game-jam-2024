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
    }

    void Update()
    {
        
    }

    public float PollTransformScaleMultipler()
    {
        float currentTransformScaleMultiplier = transformScaleMultiplier;
        return currentTransformScaleMultiplier;
    }

    public float StealTransformScaleMultipler()
    {
        float currentTransformScaleMultiplier = transformScaleMultiplier;
        UpdateTransformScale(1f);
        return currentTransformScaleMultiplier;
    }

    public void SwapTransformScaleMultiplier(Scaler source)
    {
        float currentTransformScaleMultiplier = transformScaleMultiplier;
        UpdateTransformScale(source.PollTransformScaleMultipler());
        source.UpdateTransformScale(currentTransformScaleMultiplier);
    }

    public void UpdateTransformScale(float newMultiplier)
    {
        float newXScale = gameObject.transform.localScale.x;
        float newYScale = gameObject.transform.localScale.y;
        float zScale = gameObject.transform.localScale.z;
        if (scaleTransformX)
        {
            newXScale *= newMultiplier / transformScaleMultiplier;
        }
        if (scaleTransformY)
        {
            newYScale *= newMultiplier / transformScaleMultiplier;
        }
        transformScaleMultiplier = newMultiplier;
        gameObject.transform.localScale = new Vector3(newXScale, newYScale, zScale);
    }
}
