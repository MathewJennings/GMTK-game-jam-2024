using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static System.TimeZoneInfo;

public class SceneTransitioner : MonoBehaviour
{
    [SerializeField]
    private RectTransform fader;

    private Vector3 fullyExpandedVector = new Vector3(1.5f, 1.5f, 1.5f);

    public void LoadScene(string sceneName, float transitionTime, float pauseTime)
    {
        LoadScene(sceneName, transitionTime, pauseTime, LeanTweenType.easeInOutQuint); 
    }

    public void LoadScene(string sceneName, float transitionTime, float pauseTime, LeanTweenType easeType)
    {
        LeanTween.scale(fader, fullyExpandedVector, 0);
        LeanTween.scale(fader, Vector3.zero, transitionTime / 2).setEase(easeType).setOnComplete(() =>
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
        LeanTween.scale(fader, Vector3.zero, 0);
        LeanTween.scale(fader, fullyExpandedVector, transitionBackTime).setEase(easeType);
    }
}
