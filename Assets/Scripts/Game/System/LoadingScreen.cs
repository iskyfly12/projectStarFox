using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] GameObject loadingPanel;
    [SerializeField] Image barProgress;
    [SerializeField] CanvasGroup canvasGroup;

    public float progressMultiplier_1 = 0.5f;
    public float progressMultiplier_2 = 0.07f;

    private bool isLoading = false;
    private float progressValue = 0f;

    void Update()
    {
        Load();
    }

    void Load()
    {
        if (isLoading)
        {
            if (progressValue < 1)
            {
                progressValue += progressMultiplier_1 * progressMultiplier_2;
                barProgress.fillAmount = progressValue;

                if (progressValue >= 1)
                {
                    isLoading = false;
                    progressValue = 1;
                }
            }
        }
    }

    public IEnumerator LoadNextLevel(string currentLevelName, string nextLevelName)
    {
        float delay = .5f;
        DOVirtual.Float(0, 1, delay, x => canvasGroup.alpha = x);
        loadingPanel.SetActive(true);

        yield return new WaitForSeconds(delay);

        SceneManager.UnloadSceneAsync(currentLevelName);

        isLoading = true;

        yield return new WaitWhile(() => progressValue < 1);

        AsyncOperation operation = SceneManager.LoadSceneAsync(nextLevelName, LoadSceneMode.Additive);
        
        yield return new WaitWhile(() => !operation.isDone);

        Scene nextScene = SceneManager.GetSceneByName(nextLevelName);
        SceneManager.SetActiveScene(nextScene);

        DOVirtual.Float(1, 0, delay, x => canvasGroup.alpha = x);

        yield return new WaitForSeconds(delay);

        loadingPanel.SetActive(false);
        isLoading = false;
        progressValue = 0f;
        barProgress.fillAmount = 0;

        SceneManager.UnloadSceneAsync("Loading_Scene");

        yield return null;
    }
}
