using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{

    private LevelManager levelManager;

    private void Start()
    {
        try
        {
            levelManager = FindObjectOfType<LevelManager>();
        }
        catch
        {
            // oops, no catch!
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        levelManager.LoadNextLevel();
        Debug.Log("Trigger Entered");
    }
}
