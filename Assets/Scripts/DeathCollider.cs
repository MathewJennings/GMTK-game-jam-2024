using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCollider : MonoBehaviour
{

    private LevelManager levelManager;

    private void Awake()
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
        Debug.Log("death collider trigger enter");
        levelManager.PlayerDied();
    }
}
