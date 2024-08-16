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

    // this is because we don't want to interact with the "Welcome" scene
    void Start()
    {
        LoadNextLevel();
    }
}
