using System;
using System.Collections.Generic;
using Data.Interfaces.Models.Level;
using Logic.Interfaces.Providers.Level.Grid;

namespace Logic.Providers.Level.Grid
{
	public class CellsProvider : ICellsProvider
	{
		private ICell[,] _grid;
		private int _size;

		public void Initialize(List<ICell> cells)
		{
			_size = (int)Math.Sqrt(cells.Count);
			_grid = new ICell[_size, _size];

			foreach (var cell in cells)
			{
				var x = cell.Index.x;
				var y = cell.Index.y;

				// Заполняем массив
				if (x < 0 || x >= _size || y < 0 || y >= _size)
				{
					throw new IndexOutOfRangeException($"Некорректный индекс ячейки: ({x}, {y})");
				}

				_grid[x, y] = cell;
			}
		}

		public ICell this[int x, int y] => _grid[x, y];

		public async IAsyncEnumerable<List<ICell>> GetAllColumnsAsync()
		{
			for (var x = 0; x < _size; x++)
			{
				yield return GetColumn(x);
			}
		}

		public async IAsyncEnumerable<List<ICell>> GetAllRowsAsync()
		{
			for (var y = 0; y < _size; y++)
			{
				yield return GetRow(y);
			}
		}
		
		// Получить все ячейки в столбце
		public List<ICell> GetColumn(int x)
		{
			var column = new List<ICell>();
			for (var y = 0; y < _size; y++)
			{
				column.Add(_grid[x, y]);
			}
			return column;
		}

		// Получить все ячейки в строке
		public List<ICell> GetRow(int y)
		{
			var row = new List<ICell>();
			for (var x = 0; x < _size; x++)
			{
				row.Add(_grid[x, y]);
			}
			return row;
		}
	}
}