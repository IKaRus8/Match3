
using Assets.Scripts.Logic.Interfaces.Providers.Level;
using Assets.Scripts.Logic.Interfaces.Services.Level.Grid;
using Assets.Scripts.Logic.Services.Level.Grid;
using Unity.Netcode;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Logic.Services.Level
{
	public class LevelBootstrapper : NetworkBehaviour
	{
		private IGridController _gridController;
		private ICellsProvider _cellsProvider;
		private CrystalMatchObserver _crystalMatchObserver;

		[Inject]
		private void Construct(
			IGridController gridController,
			ICellsProvider cellsProvider,
			CrystalMatchObserver crystalMatchObserver)
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

			_gridController.Initialize();

			_cellsProvider.Initialize(_gridController.Cells);

			_crystalMatchObserver.Initialize();
		}
	}
}