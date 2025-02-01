using Logic.Interfaces.Providers.Level.Grid;
using Logic.Interfaces.Services.Level.Grid;
using Logic.Providers.Level.Grid;
using Logic.Services.Level.Grid;
using UnityEngine;
using Zenject;

namespace Logic.Installers
{
    public class LevelSceneInstaller : MonoInstaller<LevelSceneInstaller>
    {
        [SerializeField]
        private Camera _mainCamera;
        
        public override void InstallBindings()
        {
            //Scene objects
            Container.Bind<IGridController>()
                .FromComponentInHierarchy()
                .AsSingle();
            Container.BindInstance(_mainCamera).AsSingle();
            Container.Bind<ICrystalMoveService>()
                .FromComponentInHierarchy()
                .AsSingle();
            
            //Services
            Container.Bind<ICrystalMatchObserver>().To<CrystalMatchObserver>().AsSingle();
            Container.Bind<CellTouchObserver>().AsSingle().NonLazy();
            
            //Providers
            Container.Bind<ICrystalsProvider>().To<CrystalsProvider>().AsSingle();
            Container.Bind<ICellsProvider>().To<CellsProvider>().AsSingle();
        }
    }
}