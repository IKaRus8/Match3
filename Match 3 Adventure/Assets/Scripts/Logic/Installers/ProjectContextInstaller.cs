using Logic.Interfaces.Services;
using Logic.Services;
using Zenject;

namespace Logic.Installers
{
	public class ProjectContextInstaller : MonoInstaller<ProjectContextInstaller>		
	{
		public override void InstallBindings()
		{
			Container.Bind<ISceneLoadService>().To<SceneLoadService>().AsSingle();
		}
	}
}