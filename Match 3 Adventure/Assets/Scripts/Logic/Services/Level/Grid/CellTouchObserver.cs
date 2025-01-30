using Data.Interfaces.Models.Level;
using UniRx;
using UnityEngine;
using Observable = UniRx.Observable;

namespace Logic.Services.Level.Grid
{
    public class CellTouchObserver
    {
        private readonly Camera _camera;

        public CellTouchObserver(Camera camera)
        {
            _camera = camera;

            Observable.EveryUpdate().Subscribe(CheckTouch);
        }
        
        private void CheckTouch(long _)
        {
            if (Input.GetMouseButtonDown(0)) // Левая кнопка мыши
            {
                // Преобразуем позицию клика в луч
                Vector2 mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
                var hit = Physics2D.Raycast(mousePosition, Vector2.zero);

                if (hit.collider != null && hit.collider.CompareTag("Cell"))
                {
                    var cell = hit.collider.GetComponent<ICell>();
                    
                    Debug.Log($"Объект нажат: {cell}");
                }
            }
        }
    }
}