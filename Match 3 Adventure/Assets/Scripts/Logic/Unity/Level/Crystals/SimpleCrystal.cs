using Assets.Scripts.Logic.Level.Unity.Grid;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Logic.Unity.Level.Crystals
{
	public class SimpleCrystal : BaseCrystal
	{
		[SerializeField]
		private CrystalTypeEnum _crystalType;

		public CrystalTypeEnum CrystalType => _crystalType;
	}
}