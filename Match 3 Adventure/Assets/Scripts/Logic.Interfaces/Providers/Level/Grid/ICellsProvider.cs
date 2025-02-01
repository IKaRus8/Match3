using System.Collections.Generic;
using Data.Interfaces.Models.Level;

namespace Logic.Interfaces.Providers.Level.Grid
{
	public interface ICellsProvider
	{
		ICell this[int x, int y] { get; }

		List<ICell> GetColumn(int x);
		List<ICell> GetRow(int y);

		IAsyncEnumerable<List<ICell>> GetAllColumnsAsync();
		IAsyncEnumerable<List<ICell>> GetAllRowsAsync();

		void Initialize(List<ICell> cells);
	}
}