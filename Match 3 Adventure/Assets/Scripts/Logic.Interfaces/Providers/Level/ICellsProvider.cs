using Assets.Scripts.Logic.Level.Unity.Grid;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Logic.Interfaces.Providers.Level
{
	public interface ICellsProvider
	{
		List<Cell> GetCellsInColumn(int x);
		List<Cell> GetCellsInRow(int y);

		IAsyncEnumerable<List<Cell>> GetAllColumnsAsync();
		IAsyncEnumerable<List<Cell>> GetAllRowsAsync();

		void Initialize(Cell[] cells);
	}
}