using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundtrackManager : MonoBehaviour
{
    [SerializeField]
    private AudioClip defaultClip;
    [SerializeField]
    private float defaultVolume;

    [SerializeField]
    private AudioClip penultimateClip;
    [SerializeField]
    private string penultimateScene;
    [SerializeField]
    private float penultimateVolume;

    [SerializeField]
    private AudioClip finalClip;
    [SerializeField]
    private string finalScene;
    [SerializeField]
    private float finalVolume;

    [SerializeField]
    private AudioSource audioSource;

    private void Start()
    {
        UpdateTrackForLevel(SceneManager.GetActiveScene().name);
        audioSource.Play();
    }

    private void PlayClip(AudioClip clip, float volume)
    {
        audioSource.volume = volume;
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
            PlayClip(penultimateClip, penultimateVolume);
        }
        else if (levelName == finalScene)
        {
            PlayClip(finalClip, finalVolume);
        }
        else
        {
            PlayClip(defaultClip, defaultVolume);
        }
    }
}
