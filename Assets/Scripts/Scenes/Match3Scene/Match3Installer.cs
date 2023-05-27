using UnityEngine;
using Zenject;

public class Match3Installer : MonoInstaller
{
    [SerializeField] private Transform BoardAnchor;
    public override void InstallBindings()
    {
        Container.Bind<Board>().AsSingle().WithArguments(BoardAnchor);
        Container.Bind<Match3Model>().AsSingle();

        Container.Bind<IInitializable>().To<Match3Initiator>().AsSingle();
    }
}