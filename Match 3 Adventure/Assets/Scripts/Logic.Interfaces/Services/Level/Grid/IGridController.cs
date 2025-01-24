using Assets.Scripts.Logic.Level.Unity.Grid;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Logic.Interfaces.Services.Level.Grid
{
	public interface IGridController 
	{
		Cell[] Cells { get; }

		event Action GridReadyEvent;

		void Initialize();
	}
}