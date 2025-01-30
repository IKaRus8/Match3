using Logic.Interfaces.Providers.Level;
using Logic.Interfaces.Services.Level.Grid;
using Logic.Providers.Level.Grid;
using Logic.Services.Level;
using Logic.Services.Level.Grid;
using Logic.Unity.Level.Grid;
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
            
            //Services
            Container.Bind<ICrystalMatchObserver>().To<CrystalMatchObserver>().AsSingle();
            Container.Bind<ICellsProvider>().To<CellsProvider>().AsSingle();
            Container.Bind<CellTouchObserver>().AsSingle().NonLazy();
        }
    }
}