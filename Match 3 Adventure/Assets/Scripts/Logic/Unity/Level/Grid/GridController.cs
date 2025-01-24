using Assets.Scripts.Logic.Interfaces.Services.Level.Grid;
using Assets.Scripts.Logic.Level.Unity.Grid;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Netcode;
using UnityEngine;
using Utilities.Extensions;

public class GridController : NetworkBehaviour, IGridController
{
	[SerializeField]
	private NetworkObject[] _crystalPrefabs;

	private Cell[] _cells;

	public Cell[] Cells => _cells;

	public event Action GridReadyEvent;

	public void Initialize()
	{
		_cells = GetComponentsInChildren<Cell>();

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

		OnGridReadyRpc();
	}

	private Cell GetEmptyCell()
	{
		return _cells.FirstOrDefault(c => c.IsEmpty());
	}

	[Rpc(SendTo.ClientsAndHost)]
	private void OnGridReadyRpc()
	{
		Debug.Log("[Grid] Grid Ready");

		GridReadyEvent?.Invoke();
	}
}
