using Assets.Scripts.Logic.Interfaces.Providers.Level;
using Assets.Scripts.Logic.Interfaces.Services.Level.Grid;
using Assets.Scripts.Logic.Level.Unity.Grid;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;

namespace Assets.Scripts.Logic.Providers.Level.Grid
{
	public class CellsProvider : ICellsProvider
	{
		private int _size;
		private readonly Dictionary<int, List<Cell>> _columns;
		private readonly Dictionary<int, List<Cell>> _rows;

		public CellsProvider()
		{
			_columns = new Dictionary<int, List<Cell>>();
			_rows = new Dictionary<int, List<Cell>>();
		}

		public void Initialize(Cell[] cells)
		{
			_size = cells.Length / cells.Length;

			foreach (var cell in cells)
			{
				int x = cell.Index.x;
				int y = cell.Index.y;

				// Добавляем ячейку в словарь столбцов
				if (!_columns.ContainsKey(x))
				{
					_columns[x] = new List<Cell>();
				}
				_columns[x].Add(cell);

				// Добавляем ячейку в словарь строк
				if (!_rows.ContainsKey(y))
				{
					_rows[y] = new List<Cell>();
				}
				_rows[y].Add(cell);
			}
		}

		public async IAsyncEnumerable<List<Cell>> GetAllColumnsAsync()
		{
			for (int i = 0; i < _size; i++)
			{
				yield return GetCellsInColumn(i);

				await UniTask.Yield();
			}
		}

		public async IAsyncEnumerable<List<Cell>> GetAllRowsAsync()
		{
			for (int i = 0; i < _size; i++)
			{
				yield return GetCellsInRow(i);

				await UniTask.Yield();
			}
		}

		// Пример использования: получить все ячейки в столбце с индексом x
		public List<Cell> GetCellsInColumn(int x)
		{
			if (_columns.ContainsKey(x))
			{
				return _columns[x];
			}
			return new List<Cell>();
		}

		// Пример использования: получить все ячейки в строке с индексом y
		public List<Cell> GetCellsInRow(int y)
		{
			if (_rows.ContainsKey(y))
			{
				return _rows[y];
			}
			return new List<Cell>();
		}
	}
}