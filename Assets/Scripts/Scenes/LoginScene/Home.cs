using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Home
{
    [Inject] private SceneLoader SceneLoader;
    private SimpleButton HomeButton;
    public Home(SimpleButton home)
    {
        HomeButton = home;
        HomeButton.OnClick += OnHomeClick;
    }
    private void OnHomeClick()
    {
        SceneLoader.PrepareScene("MainMenuScene");
    }
}
