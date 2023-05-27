using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Init : IInitializable
{
    [Inject] private SceneLoader SceneLoader;
    [Inject] private LoadingScreen LoadingScreen;
    public void Initialize()
    {
        SceneLoader.Initialize(LoadingScreen);
    }
}
