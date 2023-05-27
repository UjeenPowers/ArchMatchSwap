using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;
using static UnityEngine.AddressableAssets.Addressables;
public class ProjectInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<SceneLoader>().AsSingle().NonLazy();
        Container.Bind<Match3Stats>().AsSingle().NonLazy();
    }
}