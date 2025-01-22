using Assets.Scripts.Logic.Interfaces.Services;
using Assets.Scripts.Logic.Services;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Logic.Installers
{
	public class ProjectContextInstaller : MonoInstaller<ProjectContextInstaller>		
	{
		public override void InstallBindings()
		{
			Container.Bind<ISceneLoadService>().To<SceneLoadService>().AsSingle();
		}
	}
}