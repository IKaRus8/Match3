using Assets.Scripts.Logic.Interfaces.Services;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Logic.Services
{
	public class SceneLoadService : ISceneLoadService
	{
		public void LoadLevelScene()
		{
			SceneManager.LoadScene(1);
		}
	}
}