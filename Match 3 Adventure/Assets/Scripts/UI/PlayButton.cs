using Assets.Scripts.Logic.Interfaces.Services;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent (typeof(Button))]
public class PlayButton : MonoBehaviour
{
	[Inject]
	private readonly ISceneLoadService _sceneLoadService;

	private void Awake()
	{
		var button = GetComponent<Button>();

		button.onClick.AddListener(StartLevel);
	}

	private void StartLevel()
	{
		_sceneLoadService.LoadLevelScene();
	}
}
