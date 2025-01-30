using System;
using Data.Interfaces.Models.Level;
using Logic.Unity.Level.Grid;

namespace Logic.Interfaces.Services.Level.Grid
{
	public interface IGridController 
	{
		ICell[] Cells { get; }

		void Initialize();
	}
}