using Sirenix.OdinInspector;
using Unity.Mathematics;
using UnityEngine;

namespace Assets.Scripts.Logic.Level.Unity.Grid
{
	public class Cell : MonoBehaviour
	{
		private BaseCrystal _crystal;

		[ShowInInspector]
		public int2 Index { get; set; }

		public void SetCrystal(BaseCrystal crystal)
		{
			_crystal = crystal;
		}

		public bool IsEmpty()
		{
			return _crystal == null;
		}
	}
}