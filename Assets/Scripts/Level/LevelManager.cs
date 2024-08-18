using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class LevelManager : MonoBehaviour
{

    [SerializeField]
    private Sprite scientistWithArms;

    [SerializeField]
    private Sprite scientistWithoutArms;

    [SerializeField]
    private List<string> levelNames;

    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private GameObject pauseMenu;

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
            audioSource.Play();
            instance = this;
            DisableGuns();
            DontDestroyOnLoad(this);
        }
        else if (instance != this)
        {
            DisableGuns();
            Destroy(gameObject);
        }
    }

    private void Start()
    {

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetCurrentLevel(0.5f, 0.1f);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            TogglePauseMenu();
        }
    }

    private void TogglePauseMenu()
    {
        // Toggle time scale to freeze or resume time.
        Time.timeScale = Time.timeScale > 0 ? 0 : 1;
        if (Time.timeScale == 0)
        {
            instance.pauseMenu.SetActive(true);
        }
        else
        {
            instance.pauseMenu.SetActive(false);
        }
    }
    public void ClickMainMenu()
    {
        Time.timeScale = 1;
        instance.pauseMenu.SetActive(false);
        LoadMainMenu();
    }

    public void ClickPrevLevel()
    {
        Time.timeScale = 1;
        instance.pauseMenu.SetActive(false);
        LoadPrevLevel();
    }
    public void ClickNextLevel()
    {
        Time.timeScale = 1;
        instance.pauseMenu.SetActive(false);
        LoadNextLevel();
    }

    public void LoadMainMenu()
    {
        // TODO
    }

    public void LoadPrevLevel()
    {
        int currentLevelIndex = instance.levelNames.IndexOf(SceneManager.GetActiveScene().name);
        if (currentLevelIndex <= 0)
        {
            return;
        }
        else
        {
            SceneTransitioner sceneTransitioner = instance.gameObject.GetComponent<SceneTransitioner>();
            sceneTransitioner.LoadScene(instance.levelNames[--currentLevelIndex], 2f, 0.0f, LeanTweenType.easeInOutQuint);
        }
    }
    public void LoadNextLevel()
    {
        SceneTransitioner sceneTransitioner = instance.gameObject.GetComponent<SceneTransitioner>();
        int currentLevelIndex = instance.levelNames.IndexOf(SceneManager.GetActiveScene().name);
        if (currentLevelIndex == instance.levelNames.Count - 1)
        {
            sceneTransitioner.LoadScene("TheEnd", 2f, 0.2f, LeanTweenType.easeInExpo);
        }
        else
        {
            sceneTransitioner.LoadScene(instance.levelNames[++currentLevelIndex], 2f, 0.0f, LeanTweenType.easeInOutQuint);
        }
    }

    public void PlayerDied()
    {
        // Decoupled from resetting the level because we might want to
        // go back later and show a menu instead of resetting
        ResetCurrentLevel(1f, 0.2f);
    }

    public void ResetCurrentLevel(float transitionTime, float pauseTime)
    {
        instance.gameObject.GetComponent<SceneTransitioner>().LoadScene(SceneManager.GetActiveScene().name, transitionTime, pauseTime);
    }

    private void DisableGuns()
    {
        bool shouldSizeGunBeDisabled = instance.levelNamesWhereSizeGunShouldBeDisabled.Contains(SceneManager.GetActiveScene().name);
        bool shouldSpeedGunBeDisabled = instance.levelNamesWhereSpeedGunShouldBeDisabled.Contains(SceneManager.GetActiveScene().name);
        if (shouldSizeGunBeDisabled || shouldSpeedGunBeDisabled)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            int numPlayerChildren = player.transform.childCount;
            SpriteRenderer playerSpriteRenderer = null;
            bool usePlayerSpriteWithArms = false;
            for (int i = 0; i < numPlayerChildren; i++)
            {
                GameObject playerChild = player.transform.GetChild(i).gameObject;
                if (playerChild.tag.Equals("Gun"))
                {
                    if (shouldSizeGunBeDisabled && shouldSpeedGunBeDisabled)
                    {
                        GunShoot gunShoot = playerChild.GetComponentInChildren<GunShoot>();
                        gunShoot.DisableGunMode(GunShoot.Mode.SizeScale);
                        gunShoot.DisableGunMode(GunShoot.Mode.MovementSpeedScale);
                        playerChild.SetActive(false);
                        usePlayerSpriteWithArms = true;
                    }
                    else if (shouldSizeGunBeDisabled)
                    {
                        GunShoot gunShoot = playerChild.GetComponentInChildren<GunShoot>();
                        gunShoot.SetGunMode(GunShoot.Mode.MovementSpeedScale, true);
                        gunShoot.DisableGunMode(GunShoot.Mode.SizeScale);
                    }
                    else if (shouldSpeedGunBeDisabled)
                    {
                        GunShoot gunShoot = playerChild.GetComponentInChildren<GunShoot>();
                        gunShoot.SetGunMode(GunShoot.Mode.SizeScale, true);
                        gunShoot.DisableGunMode(GunShoot.Mode.MovementSpeedScale);
                    }
                }
                if (playerChild.name.Equals("PlayerSprite"))
                {
                    playerSpriteRenderer = playerChild.GetComponent<SpriteRenderer>();
                }
            }
            playerSpriteRenderer.sprite = usePlayerSpriteWithArms ? scientistWithArms : scientistWithoutArms;
        }
    }
}
