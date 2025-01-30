using System.Collections.Generic;
using Data.Interfaces.Models.Level;
using Logic.Unity.Level.Grid;

namespace Logic.Interfaces.Providers.Level
{
	public interface ICellsProvider
	{
		List<ICell> GetCellsInColumn(int x);
		List<ICell> GetCellsInRow(int y);

		IAsyncEnumerable<List<ICell>> GetAllColumnsAsync();
		IAsyncEnumerable<List<ICell>> GetAllRowsAsync();

		void Initialize(ICell[] cells);
	}
}