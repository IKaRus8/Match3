using System.Linq;
using Data.Interfaces.Models.Level;
using Extensions;
using Logic.Interfaces.Services.Level.Grid;
using Unity.Netcode;
using UnityEngine;

namespace Logic.Unity.Level.Grid
{
	public class GridController : NetworkBehaviour, IGridController
	{
		[SerializeField]
		private NetworkObject[] _crystalPrefabs;

		private Cell[] _cells;

		public ICell[] Cells => _cells;

		public void Initialize()
		{
			_cells = GetComponentsInChildren<Cell>();

			foreach (var cell in _cells) 
			{
				if (cell.IsEmpty)
				{
					CreateRandomCrystal(cell);
				}
			}
		}

		private void CreateRandomCrystal(Cell cell)
		{
			//cell.GetComponent<NetworkObject>().Spawn();
			
			var crystalPrefab = _crystalPrefabs.RandomElement();

			var crystalNetworkObject = NetworkManager.SpawnManager.InstantiateAndSpawn(crystalPrefab);

			crystalNetworkObject.TrySetParent(cell.NetworkObject);
			crystalNetworkObject.transform.localPosition = Vector3.zero;
			cell.SetCrystal(crystalNetworkObject.GetComponent<ICrystal>());
		}

		private Cell GetEmptyCell()
		{
			return _cells.FirstOrDefault(c => c.IsEmpty);
		}
	}
}
