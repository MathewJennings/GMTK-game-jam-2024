using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public List<string> levelNames;
    public string finalSceneName;

    private int nextLevelIndex = 0;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void LoadNextLevel()
    {
        if (nextLevelIndex >= levelNames.Count)
        {
            SceneManager.LoadScene("TheEnd");
        }
        else
        {
            SceneManager.LoadScene(levelNames[nextLevelIndex]);
            nextLevelIndex++;
        }
    }

    public void PlayerDied()
    {
        // Decoupled from resetting the level because we might want to
        // go back later and show a menu instead of resetting
        ResetCurrentLevel();
    }

    public void ResetCurrentLevel()
    {
        Debug.Log("Retrying" + levelNames[nextLevelIndex - 1].ToString());
        SceneManager.LoadScene(levelNames[nextLevelIndex - 1]);
    }

    // this is because we don't want to interact with the "Welcome" scene
    void Start()
    {
        LoadNextLevel();
    }
}
