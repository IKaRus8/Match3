using Cysharp.Threading.Tasks;
using Logic.Interfaces.Providers.Level;
using Logic.Interfaces.Services.Level.Grid;
using Unity.Netcode;
using UnityEngine;
using Zenject;

namespace Logic.Services.Level
{
	public class LevelBootstrapper : NetworkBehaviour
	{
		private IGridController _gridController;
		private ICellsProvider _cellsProvider;
		private ICrystalMatchObserver _crystalMatchObserver;

		[Inject]
		private void Construct(
			IGridController gridController,
			ICellsProvider cellsProvider,
			ICrystalMatchObserver crystalMatchObserver)
		{
			_gridController = gridController;
			_cellsProvider = cellsProvider;
			_crystalMatchObserver = crystalMatchObserver;
		}

		public override void OnNetworkSpawn()
		{
			Debug.Log("[Conection] Network Spawn");

			if (!IsServer)
			{
				return;
			}

			Debug.Log("[Conection] Server Start");

			ServerInitialization().Forget();
		}

		private async UniTaskVoid ServerInitialization()
		{
			_gridController.Initialize();

			await UniTask.Yield();

			_cellsProvider.Initialize(_gridController.Cells);

			await UniTask.Yield();

			_crystalMatchObserver.Initialize();
		}
	}
}