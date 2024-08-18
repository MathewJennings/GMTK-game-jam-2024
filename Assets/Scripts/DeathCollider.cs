using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCollider : MonoBehaviour
{

    private LevelManager levelManager;
    public AudioSource deathAudio;
    private bool dying = false;

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
        if (collision.gameObject.tag.Equals("Player") && !dying)
        {
            IEnumerator coroutine = EndCurrentLevel();
            StartCoroutine(coroutine);

        }
    }

    private IEnumerator EndCurrentLevel()
    {
        dying = true;
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<Rigidbody2D>().drag *= 10f;
        player.GetComponent<PlayerController>().enabled = false;
        deathAudio.PlayOneShot(deathAudio.clip);
        yield return new WaitForSeconds(0.5f);
        levelManager.PlayerDied();
        yield return new WaitForSeconds(1f);
        dying = false;
    }
}
