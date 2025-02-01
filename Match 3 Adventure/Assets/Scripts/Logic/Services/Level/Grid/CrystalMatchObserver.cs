using System.Collections.Generic;
using System.Linq;
using Data.Enums;
using Data.Interfaces.Models.Level;
using Logic.Interfaces.Providers.Level.Grid;
using Logic.Interfaces.Services.Level.Grid;
using UnityEngine;

namespace Logic.Services.Level.Grid
{
	public class CrystalMatchObserver : ICrystalMatchObserver
	{
		private readonly ICellsProvider _cellsProvider;

		public CrystalMatchObserver(ICellsProvider cellsProvider)
		{
			_cellsProvider = cellsProvider;
		}

		public void Initialize()
		{
			CheckGrid();
		}

		private void CheckGrid()
		{
			CheckColumns();
			CheckRows();
		}

		private async void CheckColumns()  
		{
			await foreach (var column in _cellsProvider.GetAllColumnsAsync())
			{
				CheckMatch(column.OrderBy(c => c.Index.y).ToList());
			}
		}

		private async void CheckRows()
		{
			await foreach (var row in _cellsProvider.GetAllRowsAsync())
			{
				CheckMatch(row.OrderBy(c => c.Index.x).ToList());
			}
		}

		private void CheckMatch(List<ICell> cells)
		{
			var lastType = CrystalTypeEnum.None;
			var currentSequence = new List<ICell>(); // Текущая последовательность ячеек

			foreach (var cell in cells)
			{
				var cellType = cell.CrystalType;

				if (cellType == lastType && cellType != CrystalTypeEnum.None)
				{
					currentSequence.Add(cell); // Продолжаем последовательность
				}
				else
				{
					// Если последовательность завершилась, проверяем её длину
					if (currentSequence.Count >= 3)
					{
						Match(currentSequence);
					}
                
					// Начинаем новую последовательность
					currentSequence.Clear();
					if (cellType != CrystalTypeEnum.None) // Игнорируем пустые ячейки
					{
						currentSequence.Add(cell);
						lastType = cellType;
					}
					else
					{
						lastType = CrystalTypeEnum.None;
					}
				}
			}

			// Проверяем последовательность после окончания колонки
			if (currentSequence.Count >= 3)
			{
				Match(currentSequence);
			}
		}

		private void Match(List<ICell> matchedCells)
		{
			// Собираем индексы ячеек в строку
			var indices = matchedCells
				.Select(cell => cell.ToString()) // Предполагается, что Index реализован как int2
				.ToArray();

			var result = $"match {string.Join(", ", indices)}";
			Debug.Log(result);
		}
	}
}