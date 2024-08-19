using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundtrackManager : MonoBehaviour
{
    [SerializeField]
    private AudioClip defaultClip;

    [SerializeField]
    private AudioClip penultimateClip;
    [SerializeField]
    private string penultimateScene;

    [SerializeField]
    private AudioClip finalClip;
    [SerializeField]
    private string finalScene;

    [SerializeField]
    private AudioSource audioSource;

    private void Start()
    {
        UpdateTrackForLevel(SceneManager.GetActiveScene().name);
        audioSource.Play();
    }

    private void PlayClip(AudioClip clip)
    {
        if (audioSource.clip != clip)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
        
    }

    public void UpdateTrackForLevel(string levelName)
    {
        if (levelName == penultimateScene)
        {
            PlayClip(penultimateClip);
        }
        else if (levelName == finalScene)
        {
            PlayClip(finalClip);
        }
        else
        {
            PlayClip(defaultClip);
        }
    }
}
