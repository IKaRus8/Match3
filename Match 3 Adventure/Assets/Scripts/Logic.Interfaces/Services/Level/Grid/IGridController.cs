using System.Collections.Generic;
using Data.Interfaces.Models.Level;

namespace Logic.Interfaces.Services.Level.Grid
{
	public interface IGridController 
	{
		List<ICell> Initialize();
	}
}