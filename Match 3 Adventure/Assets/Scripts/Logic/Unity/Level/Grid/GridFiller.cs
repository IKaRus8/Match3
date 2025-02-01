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
			ClearGrid();

			// Рассчитываем смещение для центрирования сетки
			float centerOffset = (_gridSize - 1) * _offset * 0.5f;

			for (int x = 0; x < _gridSize; x++)
			{
				for (int y = 0; y < _gridSize; y++)
				{
					// Позиция с учетом центрирования
					float posX = x * _offset - centerOffset;
					float posY = y * _offset - centerOffset;

					// Создаем ячейку
					GameObject cell = (GameObject)PrefabUtility.InstantiatePrefab(_cellPrefab, transform);
					cell.transform.position = new Vector3(posX, posY, 0);
					cell.name = $"Cell_{x}_{y}";

					// Присваиваем индекс (0,0) будет в левом нижнем углу
					cell.GetComponent<Cell>().Index = new int2(x, y);
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