using Logic.Interfaces.Services;
using UnityEngine.SceneManagement;

namespace Logic.Services
{
	public class SceneLoadService : ISceneLoadService
	{
		public void LoadLevelScene()
		{
			SceneManager.LoadScene(1);
		}
	}
}