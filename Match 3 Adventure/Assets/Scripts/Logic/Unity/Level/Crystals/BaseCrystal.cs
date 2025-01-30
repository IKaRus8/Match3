using Cysharp.Threading.Tasks;
using Data;
using Data.Interfaces.Models.Level;
using Unity.Netcode;
using UnityEngine;

namespace Logic.Unity.Level.Crystals
{
	public class BaseCrystal : NetworkBehaviour, ICrystal
	{
		[SerializeField]
		protected CrystalTypeEnum _crystalType;

		public CrystalTypeEnum CrystalType => _crystalType;

		public async UniTask Move(Vector3 position)
		{
			
		}
	}
}