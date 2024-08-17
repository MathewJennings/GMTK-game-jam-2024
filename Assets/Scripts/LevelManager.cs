using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    [SerializeField]
    private List<string> levelNames;

    [SerializeField]
    private List<string> levelNamesWhereSizeGunShouldBeDisabled;

    [SerializeField]
    private List<string> levelNamesWhereSpeedGunShouldBeDisabled;

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
    }

    private void Start()
    {
        bool shouldSizeGunBeDisabled = instance.levelNamesWhereSizeGunShouldBeDisabled.Contains(SceneManager.GetActiveScene().name);
        bool shouldSpeedGunBeDisabled = instance.levelNamesWhereSpeedGunShouldBeDisabled.Contains(SceneManager.GetActiveScene().name);
        if (shouldSizeGunBeDisabled || shouldSpeedGunBeDisabled)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            int numPlayerChildren = player.transform.childCount;
            for (int i = 0; i < numPlayerChildren; i++)
            {
                GameObject playerChild = player.transform.GetChild(i).gameObject;
                if (playerChild.tag.Equals("Gun"))
                {
                    if (shouldSizeGunBeDisabled && shouldSpeedGunBeDisabled)
                    {
                        playerChild.SetActive(false);
                    }
                    else if (shouldSizeGunBeDisabled)
                    {
                        GunShoot gunShoot = playerChild.GetComponentInChildren<GunShoot>();
                        gunShoot.SetGunMode(GunShoot.Mode.MovementSpeedScale);
                        gunShoot.DisableGunMode(GunShoot.Mode.SizeScale);
                    }
                    else if (shouldSpeedGunBeDisabled)
                    {
                        GunShoot gunShoot = playerChild.GetComponentInChildren<GunShoot>();
                        gunShoot.SetGunMode(GunShoot.Mode.SizeScale);
                        gunShoot.DisableGunMode(GunShoot.Mode.MovementSpeedScale);
                    }
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
