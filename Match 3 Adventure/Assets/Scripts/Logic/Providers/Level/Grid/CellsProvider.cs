using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Data.Interfaces.Models.Level;
using Logic.Interfaces.Providers.Level;
using Unity.Mathematics;
using UnityEngine;

namespace Logic.Providers.Level.Grid
{
	public class CellsProvider : ICellsProvider
	{
		private readonly Dictionary<int, List<ICell>> _columns;
		private readonly Dictionary<int, List<ICell>> _rows;
		
		private int _size;

		public CellsProvider()
		{
			_columns = new Dictionary<int, List<ICell>>();
			_rows = new Dictionary<int, List<ICell>>();
		}

		public void Initialize(ICell[] cells)
		{
			_size = (int)math.sqrt(cells.Length);

			foreach (var cell in cells)
			{
				var x = cell.Index.x;
				var y = cell.Index.y;

				// Добавляем ячейку в словарь столбцов
				if (!_columns.ContainsKey(x))
				{
					_columns[x] = new List<ICell>();
				}
				
				_columns[x].Add(cell);

				// Добавляем ячейку в словарь строк
				if (!_rows.ContainsKey(y))
				{
					_rows[y] = new List<ICell>();
				}
				
				_rows[y].Add(cell);
			}
			
			Debug.Log($"Columns: {_columns.Count} \n Rows: {_rows.Count}");
		}

		public async IAsyncEnumerable<List<ICell>> GetAllColumnsAsync()
		{
			foreach (var key in _columns.Keys)
			{
				yield return GetCellsInColumn(key);

				await UniTask.Yield();
			}
		}

		public async IAsyncEnumerable<List<ICell>> GetAllRowsAsync()
		{
			foreach (var key in _rows.Keys)
			{
				yield return GetCellsInRow(key);

				await UniTask.Yield();
			}
		}

		// Пример использования: получить все ячейки в столбце с индексом x
		public List<ICell> GetCellsInColumn(int x)
		{
			if (_columns.TryGetValue(x, out var column))
			{
				return column;
			}
			
			return new List<ICell>();
		}

		// Пример использования: получить все ячейки в строке с индексом y
		public List<ICell> GetCellsInRow(int y)
		{
			if (_rows.TryGetValue(y, out var row))
			{
				return row;
			}
			
			return new List<ICell>();
		}
	}
}