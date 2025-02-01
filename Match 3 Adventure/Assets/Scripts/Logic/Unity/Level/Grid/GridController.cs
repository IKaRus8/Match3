using System.Collections.Generic;
using System.Linq;
using Data.Interfaces.Models.Level;
using Logic.Interfaces.Providers.Level.Grid;
using Logic.Interfaces.Services.Level.Grid;
using Unity.Netcode;
using Zenject;

namespace Logic.Unity.Level.Grid
{
	public class GridController : NetworkBehaviour, IGridController
	{
		private ICrystalsProvider _crystalsProvider;

		[Inject]
		private void Construct(ICrystalsProvider crystalsProvider)
		{
			_crystalsProvider = crystalsProvider;
		}

		public List<ICell> Initialize()
		{
			var cells = GetComponentsInChildren<ICell>().ToList();

			foreach (var cell in cells) 
			{
				if (cell.IsEmpty)
				{
					CreateRandomCrystal(cell);
				}
			}

			return cells;
		}

		private void CreateRandomCrystal(ICell cell)
		{
			var crystalPrefab = _crystalsProvider.GetRandomCrystalPrefab();

			var crystalNetworkObject = NetworkManager.SpawnManager.InstantiateAndSpawn(crystalPrefab);

			cell.SetCrystal(crystalNetworkObject.GetComponent<ICrystal>());
		}
	}
}
