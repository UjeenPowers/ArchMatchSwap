using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MainMenu
{
    [Inject] private SceneLoader SceneLoader;
    private SimpleButton PlayButtob;
    public MainMenu(SimpleButton play)
    {
        PlayButtob = play;
        PlayButtob.OnClick += OnPlayClick;
    }
    private void OnPlayClick()
    {
        SceneLoader.PrepareScene("Match3Scene");
    }
}
