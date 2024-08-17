using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementSpeedScaler : MonoBehaviour
{
    [SerializeField]
    private float multiplier = 1f;

    public float getMultiplier()
    {
        return multiplier;
    }

    public float PollMultipler()
    {
        float currentMultiplier = multiplier;
        return currentMultiplier;
    }

    public float StealMultipler()
    {
        float currentMultiplier = multiplier;
        UpdateMultiplier(1f);
        return currentMultiplier;
    }

    public void SwapMultiplier(MovementSpeedScaler source)
    {
        float currentMultiplier = multiplier;
        UpdateMultiplier(source.PollMultipler());
        source.UpdateMultiplier(currentMultiplier);
    }

    public void UpdateMultiplier(float newMultiplier)
    {
        multiplier = newMultiplier;
    }
}
