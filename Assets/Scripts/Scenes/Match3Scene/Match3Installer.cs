using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class Match3Installer : MonoInstaller
{
    [SerializeField] private Transform BoardAnchor;
    [SerializeField] private EventSystem EventSystem;
    public override void InstallBindings()
    {
        Container.Bind<ActionSystem>().AsSingle();
        Container.Bind<AnimationChainer>().AsSingle();
        Container.Bind<InputController>().AsSingle().WithArguments(EventSystem);
        Container.Bind<Board>().AsSingle().WithArguments(BoardAnchor);
        Container.Bind<Match3Model>().AsSingle();

        Container.Bind<IInitializable>().To<Match3Initiator>().AsSingle();
    }
}