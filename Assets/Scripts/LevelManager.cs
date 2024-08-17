using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    [SerializeField]
    private List<string> levelNames;

    [SerializeField]
    private List<string> levelNamesWhereGunShouldBeDisabled;

    // Singleton LevelManager
    private static LevelManager instance;

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

        if (instance.levelNamesWhereGunShouldBeDisabled.Contains(SceneManager.GetActiveScene().name))
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            int numPlayerChildren = player.transform.childCount;
            for (int i = 0; i < numPlayerChildren; i++)
            {
                GameObject playerChild = player.transform.GetChild(i).gameObject;
                if (playerChild.tag.Equals("Gun"))
                {
                    playerChild.SetActive(false);
                }
            }
        }
    }

    public void LoadNextLevel()
    {
        int currentLevelIndex = instance.levelNames.IndexOf(SceneManager.GetActiveScene().name);
        if (currentLevelIndex == instance.levelNames.Count - 1)
        {
            SceneManager.LoadScene("TheEnd");
        }
        else
        {
            SceneManager.LoadScene(instance.levelNames[++currentLevelIndex]);
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
