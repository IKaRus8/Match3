using Cysharp.Threading.Tasks;
using Data.Enums;
using Data.Interfaces.Models.Level;
using DG.Tweening;
using Unity.Netcode;
using UnityEngine;

namespace Logic.Unity.Level.Crystals
{
	public class BaseCrystal : NetworkBehaviour, ICrystal
	{
		[SerializeField]
		protected CrystalTypeEnum _crystalType;

		public ulong CrystalId => NetworkObjectId;
		public CrystalTypeEnum CrystalType => _crystalType;
		
		public async UniTask MoveToCell(ICell cell)
		{
			Debug.Log($"Crystal {_crystalType} Move To {cell}");
			
			NetworkObject.TrySetParent(cell.NetworkObject);
			
			await transform.DOLocalMove(Vector3.zero, 1f).AsyncWaitForCompletion();
		}
	}
}