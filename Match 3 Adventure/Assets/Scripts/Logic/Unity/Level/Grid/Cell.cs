using Data;
using Data.Interfaces.Models.Level;
using Unity.Mathematics;
using Unity.Netcode;
using UnityEngine;

namespace Logic.Unity.Level.Grid
{
	public class Cell : NetworkBehaviour, ICell
	{
		private ICrystal _crystal;

		[SerializeField]
		private int2 _index;

		public int2 Index
		{
			get => _index;
			set => _index = value;
		}
		public ICrystal Content => _crystal;
		public bool IsEmpty => _crystal == null;
		public CrystalTypeEnum CrystalType => GetCrystalType();

		public void SetCrystal(ICrystal crystal)
		{
			_crystal = crystal;
		}

		private CrystalTypeEnum GetCrystalType()
		{
			if (_crystal == null)
			{
				return CrystalTypeEnum.None;
			}
			
			return _crystal.CrystalType;
		}

		public override string ToString()
		{
			return $"Cell [{_index.x}, {_index.y}] is {CrystalType}";
		}
	}
}