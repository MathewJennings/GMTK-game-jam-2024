using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using static System.TimeZoneInfo;

public class SceneTransitioner : MonoBehaviour
{
    [SerializeField]
    private RectTransform fader;

    [SerializeField]
    private TextMeshProUGUI levelText;

    private Vector3 fullyExpandedVector = new Vector3(1.5f, 1.5f, 1.5f);

    private void Awake()
    {
        levelText.text = SceneManager.GetActiveScene().name.Replace('_', ' ');
        levelText.gameObject.SetActive(true);
        Invoke("DisableLevelText", 5f);
    }

    public void LoadScene(string sceneName, float transitionTime, float pauseTime)
    {
        LoadScene(sceneName, transitionTime, pauseTime, LeanTweenType.easeInOutQuint); 
    }

    public void LoadScene(string sceneName, float transitionTime, float pauseTime, LeanTweenType easeType)
    {
        LeanTween.scale(fader, fullyExpandedVector, 0);
        LeanTween.scale(fader, Vector3.zero, transitionTime / 2).setEase(easeType).setOnComplete(() =>
        {
            levelText.gameObject.SetActive(true);
            TriggerWaitAndLoadScene(sceneName, easeType, pauseTime, transitionTime / 2);
        });
    }

    private Coroutine TriggerWaitAndLoadScene(string sceneName, LeanTweenType easeType, float pauseTime, float transitionBackTime)
    {
        return StartCoroutine(WaitAndLoadScene(sceneName, easeType, pauseTime, transitionBackTime));
    }

    private IEnumerator WaitAndLoadScene(string sceneName, LeanTweenType easeType, float pauseTime, float transitionBackTime)
    {
        levelText.text = sceneName.Replace('_', ' ');
        yield return new WaitForSeconds(pauseTime);
        SceneManager.LoadScene(sceneName);
        LeanTween.scale(fader, Vector3.zero, 0);
        LeanTween.scale(fader, fullyExpandedVector, transitionBackTime).setEase(easeType).setOnComplete(() =>
        {
            Invoke("DisableLevelText", 3f);
        });
    }
    void DisableLevelText()
    {
        levelText.gameObject.SetActive(false);
    }
}
