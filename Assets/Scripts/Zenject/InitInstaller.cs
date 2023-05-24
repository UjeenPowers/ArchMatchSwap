using UnityEngine;
using Zenject;

public class InitInstaller : MonoInstaller
{
    [SerializeField] private LoadingScreen LoadingScreen;
    public override void InstallBindings()
    {
        Container.Bind<LoadingScreen>().FromInstance(LoadingScreen);
        Container.Bind<IInitializable>().To<Init>().AsSingle();
    }
}