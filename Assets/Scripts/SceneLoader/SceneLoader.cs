using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;
using Zenject;
using DG.Tweening;
using UnityEngine.ResourceManagement.ResourceProviders;

public class SceneLoader
{
    private LoadingScreen LoadingScreen;
    private Scene CurrentScene;
    public void Initialize(LoadingScreen loadingScreen)
    {
        LoadingScreen = loadingScreen;
        PrepareScene("LoginScene");
    }
    public void PrepareScene(string key)
    {
        Debug.Log($"Preparing scene with a key {key}");
        var operation = Addressables.LoadSceneAsync(key,LoadSceneMode.Single,false);
        LoadingScreen.AnimateLoading(operation, () => StartScene(operation));
        // operation.Completed += StartScene;
    }
    private void StartScene(AsyncOperationHandle<SceneInstance> operation)
    {
        operation.Result.ActivateAsync();
        CurrentScene = operation.Result.Scene;
        Debug.Log($"Scene displayed");
    }
}
