using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class SizeScaler : MonoBehaviour
{
    // How much the GameObject's transform should be scaled
    [SerializeField]
    private float transformScaleMultiplier = 1f;
    // Should the X Scale be changed?
    [SerializeField]
    private bool scaleTransformX = false;
    // Should the Y Scale be changed?
    [SerializeField]
    private bool scaleTransformY = false;
    // How long it should take for the object to complete a change in scale
    [SerializeField]
    private float scaleTimeInSecs = 0.5f;
    [SerializeField]
    private Button indicatorButton;

    // True only while we are in the process of changing the scale
    private bool scaleIsChanging = false;
    // If a scale change is occurring, how far are we through it? 0-1 = 0-100%
    private float scaleTimeElapsedPercentage = 0.0f;
    // If a scale change is occurring, this is where we started
    private Vector3 initialLocalScale = Vector3.zero;
    // If a scale change is occurring, this is where we want to end up
    private Vector3 targetLocalScale = Vector3.zero;

    void Start()
    {
    }

    public float GetTransformScaleMultiplier()
    {
        return transformScaleMultiplier;
    }

    public bool GetScaleIsChanging()
    {
        return scaleIsChanging;
    }

    public bool DoesScale()
    {
        return scaleTransformX || scaleTransformY;
    }

    public bool IsScaled()
    {
        return 1f != transformScaleMultiplier;
    }

    public void SwapTransformScaleMultiplier(SizeScaler other)
    {
        float myMultiplier = transformScaleMultiplier;
        UpdateTransformScale(other.transformScaleMultiplier);
        other.UpdateTransformScale(myMultiplier);
    }

    public void Spaghettify(float newXMultiplier, float newYMultiplier)
    {
        float newXScale = gameObject.transform.localScale.x;
        float newYScale = gameObject.transform.localScale.y;
        float zScale = gameObject.transform.localScale.z;
        if (scaleTransformX)
        {
            newXScale *= newXMultiplier / transformScaleMultiplier;
        }
        if (scaleTransformY)
        {
            newYScale *= newYMultiplier / transformScaleMultiplier;
        }
        StartSmoothScaling(newXScale, newYScale, zScale);
    }

    private void UpdateTransformScale(float newMultiplier)
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
        if (indicatorButton != null)
        {
            indicatorButton.gameObject.SetActive(IsScaled());
        }
        StartSmoothScaling(newXScale, newYScale, zScale);
    }

    private void StartSmoothScaling(float targetX, float targetY, float targetZ)
    {
        scaleIsChanging = true;
        scaleTimeElapsedPercentage = 0.0f;
        initialLocalScale = gameObject.transform.localScale;
        targetLocalScale = new Vector3(targetX, targetY, targetZ);
    }

    void Update()
    {
        if (scaleIsChanging)
        {
            UpdateScale();
        }
    }

    private void UpdateScale()
    {
        scaleTimeElapsedPercentage += Time.deltaTime / scaleTimeInSecs;
        gameObject.transform.localScale = Vector3.Slerp(initialLocalScale, targetLocalScale, scaleTimeElapsedPercentage);
        if (scaleTimeElapsedPercentage >= 1.0f)
        {
            CompleteSmoothScaling();
        }
    }

    private void CompleteSmoothScaling()
    {
        UnityEngine.Debug.Log("HIT");
        scaleIsChanging = false;
        scaleTimeElapsedPercentage = 1.0f;
        initialLocalScale = Vector3.zero;
        targetLocalScale = Vector3.zero;
    }
}
