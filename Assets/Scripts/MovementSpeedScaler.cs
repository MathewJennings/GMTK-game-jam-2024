using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovementSpeedScaler : MonoBehaviour
{
    [SerializeField]
    private float multiplier = 1f;
    [SerializeField]
    private Button indicatorButton;

    public float getMultiplier()
    {
        return multiplier;
    }

    public bool IsScaled()
    {
        return 1f != multiplier;
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
        indicatorButton.gameObject.SetActive(IsScaled());
    }
}
