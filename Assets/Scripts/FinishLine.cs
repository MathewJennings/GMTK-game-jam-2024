using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    public AudioSource audioSource;
    private LevelManager levelManager;
    private bool haveFinished;

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
        if (collision.gameObject.tag.Equals("Player") && !haveFinished)
        {
            EndCurrentLevel();
        }
    }
    private void EndCurrentLevel()
    {
        haveFinished = true;
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<Rigidbody2D>().drag *= 10f;
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        player.GetComponent<Rigidbody2D>().gravityScale = 0.5f;
        player.GetComponent<Rigidbody2D>().angularVelocity = 70f;
        player.GetComponent<SizeScaler>().Spaghettify(.5f, 2.5f);
        audioSource.PlayOneShot(audioSource.clip, 1f);
        levelManager.LoadNextLevel();
    }
}
