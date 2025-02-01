using Data.Enums;
using Data.Interfaces.Models.Level;
using Unity.Mathematics;
using Unity.Netcode;
using UnityEngine;

namespace Logic.Unity.Level.Grid
{
	public class Cell : NetworkBehaviour, ICell
	{
		[SerializeField]
		private int2 _index;
		
		private readonly Vector3 _selectedScale = new Vector3(1.2f, 1.2f, 1.2f);
		
		private NetworkVariable<ulong> _crystalId;
		private NetworkVariable<CrystalTypeEnum> _crystalType;

		public ulong CrystalId => _crystalId.Value;
		public bool IsEmpty => _crystalId.Value == default;
		public CrystalTypeEnum CrystalType => _crystalType.Value;
		public int2 Index
		{
			get => _index;
			set => _index = value;
		}

		private void Awake()
		{
			_crystalId = new NetworkVariable<ulong>();
			_crystalType = new NetworkVariable<CrystalTypeEnum>();
		}

		public void SetCrystal(ICrystal crystal)
		{
			SetCrystalRpc(crystal.CrystalType, crystal.CrystalId);
			
			crystal.MoveToCell(this);
		}

		[Rpc(SendTo.Server)]
		private void SetCrystalRpc(CrystalTypeEnum crystalType, ulong crystalId)
		{
			_crystalId.Value = crystalId;
			_crystalType.Value = crystalType;
		}

		public void Select(bool value)
		{
			transform.localScale = value ? _selectedScale : Vector3.one;
		}

		public override string ToString()
		{
			return $"Cell [{_index.x}, {_index.y}] is {CrystalType}";
		}
	}
}