using UnityEngine;
using Zenject;

public class MainMenuInstaller : MonoInstaller
{
    [SerializeField] private SimpleButton SimpleButton;
    public override void InstallBindings()
    {
        Container.Bind<MainMenu>().AsSingle().WithArguments(SimpleButton).NonLazy();
    }
}