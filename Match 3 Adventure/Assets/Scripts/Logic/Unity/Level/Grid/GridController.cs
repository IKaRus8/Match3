using Assets.Scripts.Logic.Level.Unity.Grid;
using Sirenix.OdinInspector;
using System.Linq;
using Unity.Netcode;
using UnityEngine;
using Utilities.Extensions;

public class GridController : NetworkBehaviour
{
	[SerializeField]
	private NetworkObject[] _crystalPrefabs;

	private Cell[] _cells;

	private void Awake()
	{
		_cells = GetComponentsInChildren<Cell>();
	}

	public override void OnNetworkSpawn()
	{
		Debug.Log("[Conection] Network Spawn");

		if (IsServer)
		{
			Debug.Log("[Conection] Server Start");

			FillStartGrid();
		}
	}

	private void FillStartGrid()
	{
		foreach (var cell in _cells) 
		{
			if (cell.IsEmpty())
			{
				CreateRandomCrystal(cell);
			}
		}
	}

	private void CreateRandomCrystal(Cell cell)
	{
		var cellPrefab = _crystalPrefabs.RandomElement();

		var cellNetworkObject = NetworkManager.SpawnManager.InstantiateAndSpawn(cellPrefab);

		cellNetworkObject.transform.SetParent(cell.transform, false);
		cell.SetCrystal(cellNetworkObject.GetComponent<BaseCrystal>());
	}

	private Cell GetEmptyCell()
	{
		return _cells.FirstOrDefault(c => c.IsEmpty());
	}
}
