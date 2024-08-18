using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static System.TimeZoneInfo;

public class SceneTransitioner : MonoBehaviour
{
    [SerializeField]
    private RectTransform fader;

    public void LoadScene(string sceneName, float transitionTime, float pauseTime)
    {
        LoadScene(sceneName, transitionTime, pauseTime, LeanTweenType.easeInOutQuint); 
    }

    public void LoadScene(string sceneName, float transitionTime, float pauseTime, LeanTweenType easeType)
    {
        fader.gameObject.SetActive(true);
        LeanTween.scale(fader, Vector3.zero, 0);
        LeanTween.scale(fader, new Vector3(1, 1, 1), transitionTime / 2).setEase(easeType).setOnComplete(() =>
        {
            TriggerWaitAndLoadScene(sceneName, easeType, pauseTime, transitionTime / 2);
        });
    }

    private Coroutine TriggerWaitAndLoadScene(string sceneName, LeanTweenType easeType, float pauseTime, float transitionBackTime)
    {
        return StartCoroutine(WaitAndLoadScene(sceneName, easeType, pauseTime, transitionBackTime));
    }

    private IEnumerator WaitAndLoadScene(string sceneName, LeanTweenType easeType, float pauseTime, float transitionBackTime)
    {
        yield return new WaitForSeconds(pauseTime);
        SceneManager.LoadScene(sceneName);
        LeanTween.scale(fader, Vector3.zero, transitionBackTime).setEase(easeType).setOnComplete(() =>
        {
            fader.gameObject.SetActive(false);
        });
    }
}
