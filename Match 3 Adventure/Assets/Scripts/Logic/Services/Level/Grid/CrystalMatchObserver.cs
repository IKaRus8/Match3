using Assets.Scripts.Logic.Interfaces.Providers.Level;
using UniRx;

namespace Assets.Scripts.Logic.Services.Level.Grid
{
	public class CrystalMatchObserver 
	{
		private readonly ICellsProvider _cellsProvider;

		public CrystalMatchObserver(ICellsProvider cellsProvider)
		{
			_cellsProvider = cellsProvider;
		}

		public void Initialize()
		{
			Observable.EveryFixedUpdate().Subscribe(CheckGrid);
		}

		private void CheckGrid(long _)
		{
			CheckColumns();
			CheckRows();
		}

		private async void CheckColumns()  
		{
			await foreach (var column in _cellsProvider.GetAllColumnsAsync())
			{
				foreach (var cell in column)
				{

				}
			}
		}

		private async void CheckRows()
		{
			await foreach (var column in _cellsProvider.GetAllRowsAsync())
			{
				foreach (var cell in column)
				{

				}
			}
		}
	}
}