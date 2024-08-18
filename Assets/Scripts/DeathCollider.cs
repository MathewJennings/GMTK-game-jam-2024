using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCollider : MonoBehaviour
{

    private LevelManager levelManager;
    public AudioSource deathAudio;

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
        if (collision.gameObject.tag.Equals("Player"))
        {
            IEnumerator coroutine = EndCurrentLevel(deathAudio.clip.length);
            StartCoroutine(coroutine);

        }
    }

    private IEnumerator EndCurrentLevel(float waitTime)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<Rigidbody2D>().drag *= 10f;
        player.GetComponent<PlayerController>().enabled = false;
        deathAudio.PlayOneShot(deathAudio.clip);
        yield return new WaitForSeconds(waitTime);
        levelManager.PlayerDied();
    }
}
