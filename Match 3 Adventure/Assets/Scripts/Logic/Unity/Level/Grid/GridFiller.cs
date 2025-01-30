using Sirenix.OdinInspector;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;

namespace Logic.Unity.Level.Grid
{
	public class GridFiller : MonoBehaviour
	{
		[SerializeField]
		private int _gridSize = 8;
		[SerializeField]
		private float _offset = 1f;
		[SerializeField]
		private GameObject _cellPrefab;

		[Button]// Добавляем кнопку в инспектор через Odin
		private void FillGrid()
		{
			// Очищаем старые ячейки, если они есть
			ClearGrid();

			// Рассчитываем смещение для центрирования сетки
			int halfSize = _gridSize / 2;

			// Заполняем сетку
			for (int x = 0; x < _gridSize; x++)
			{
				for (int y = 0; y < _gridSize; y++)
				{
					// Рассчитываем позицию для текущей ячейки
					int posX = x - halfSize;
					int posY = y - halfSize;

					// Создаем ячейку как клон префаба
					Vector3 cellPosition = new Vector3(posX * _offset, posY * _offset, 0);
					GameObject cell = (GameObject)PrefabUtility.InstantiatePrefab(_cellPrefab, transform);
					cell.transform.position = cellPosition;
					cell.name = $"Cell_{posX}_{posY}"; // Именуем ячейку для удобства

					cell.GetComponent<Cell>().Index = new int2(posX, posY);
				}
			}
		}

		[Button]
		private void ClearGrid()
		{
			// Удаляем все дочерние объекты (старые ячейки)
			while (transform.childCount > 0)
			{
				DestroyImmediate(transform.GetChild(0).gameObject);
			}
		}
	}
}