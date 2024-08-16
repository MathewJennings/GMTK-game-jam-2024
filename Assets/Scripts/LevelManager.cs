using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private static LevelManager instance;

    public List<string> levelNames;
    public string finalSceneName;

    public int currentLevelIndex;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else if (instance != this)
        {
            Destroy(this);
        }
    }

    public void LoadNextLevel()
    {
        if (instance.currentLevelIndex == instance.levelNames.Count - 1)
        {
            SceneManager.LoadScene("TheEnd");
        }
        else
        {
            instance.currentLevelIndex++;
            SceneManager.LoadScene(instance.levelNames[instance.currentLevelIndex]);
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
        Debug.Log("Retrying" + instance.levelNames[instance.currentLevelIndex].ToString());
        SceneManager.LoadScene(instance.levelNames[instance.currentLevelIndex]);
    }
}
