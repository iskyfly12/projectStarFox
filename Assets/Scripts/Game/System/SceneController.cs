using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    private readonly string string_LoadingScene = "Loading_Scene";

    public void OnChangeScene(string sceneName)
    {
        StartCoroutine(ChangeScene(sceneName));
    }

    public void OnCloseGame()
    {
        Application.Quit();
    }

    IEnumerator ChangeScene(string sceneName)
    {
        Scene currentLevel = SceneManager.GetActiveScene();
        string currentLevelName = currentLevel.name;

        if (!SceneManager.GetSceneByName(string_LoadingScene).isLoaded)
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(string_LoadingScene, LoadSceneMode.Additive);
            yield return new WaitUntil(() => operation.isDone);
        }

        Scene loadingScene = SceneManager.GetSceneByName(string_LoadingScene);
        SceneManager.SetActiveScene(loadingScene);

        LoadingScreen loading = FindObjectOfType<LoadingScreen>();

        loading.StartCoroutine(loading.LoadNextLevel(currentLevelName, sceneName));

        yield break;
    }
}
