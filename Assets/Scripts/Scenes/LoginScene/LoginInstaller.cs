using UnityEngine;
using Zenject;

public class LoginInstaller : MonoInstaller
{
    [SerializeField] private SimpleButton Home;
    public override void InstallBindings()
    {
        Container.Bind<Home>().AsSingle().WithArguments(Home).NonLazy();
    }
}