using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    public AudioSource audioSource;
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
        if (collision.gameObject.GetComponent<PlayerController>() != null)
        {
            IEnumerator coroutine = EndCurrentLevel(audioSource.clip.length);
            StartCoroutine(coroutine);
        }
    }
    private IEnumerator EndCurrentLevel(float waitTime)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<PlayerController>().enabled = false;
        audioSource.PlayOneShot(audioSource.clip, 1f);
        yield return new WaitForSeconds(waitTime);
        levelManager.LoadNextLevel();
    }
}
