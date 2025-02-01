using Data.Interfaces.Models.Level;
using Logic.Interfaces.Providers.Level.Grid;
using Logic.Interfaces.Services.Level.Grid;
using Unity.Mathematics;
using Unity.Netcode;
using UnityEngine;
using Zenject;

namespace Logic.Services.Level.Grid
{
    public class CrystalMoveService : NetworkBehaviour, ICrystalMoveService
    {
        private ICrystalsProvider _crystalsProvider;
        private ICellsProvider _cellsProvider;

        private ICell _selectedCell;
        
        [Inject]
        private void Construct(
            ICrystalsProvider crystalsProvider,
            ICellsProvider cellsProvider)
        {
            _crystalsProvider = crystalsProvider;
            _cellsProvider = cellsProvider;
        }
        
        public void CellSelected(ICell cell)
        {
            if(_selectedCell == null)
            {
                SelectCell(cell);
                
                return;
            }

            if (_selectedCell == cell)
            {
                CancelSelect();
                
                return;
            }
            
            SwitchCrystals(_selectedCell, cell);
        }

        public void SwitchCrystals(ICell startCell, ICell endCell)
        {
            SwitchCrystalsRpc(new SwitchCrystalModel(
                startCell.Index,
                endCell.Index, 
                startCell.CrystalId, 
                endCell.CrystalId));
            
            CancelSelect();
        }

        [Rpc(SendTo.Server)]
        private void SwitchCrystalsRpc(SwitchCrystalModel model)
        {
            Debug.Log($"SwitchCrystal");
            
            var startCellCrystal = _crystalsProvider.GetSpawnedCrystal(model._startCrystalId);
            var endCellCrystal = _crystalsProvider.GetSpawnedCrystal(model._endCrystalId);
            
            var startCell = _cellsProvider[model._startCellIndex.x, model._startCellIndex.y];
            var endCell = _cellsProvider[model._endCellIndex.x, model._endCellIndex.y];
            
            endCell.SetCrystal(startCellCrystal);
            startCell.SetCrystal(endCellCrystal);
        }

        private void SelectCell(ICell cell)
        {
            if (_selectedCell == cell)
            {
                return;
            }
            
            _selectedCell = cell;
            _selectedCell.Select(true);
        }

        private void CancelSelect()
        {
            _selectedCell.Select(false);
            _selectedCell = null;
        }

        private struct SwitchCrystalModel : INetworkSerializable
        {
            public int2 _startCellIndex;
            public int2 _endCellIndex;
            
            public ulong _startCrystalId;
            public ulong _endCrystalId;

            public SwitchCrystalModel(
                int2 startCellIndex, 
                int2 endCellIndex, 
                ulong startCrystalId, 
                ulong endCrystalId)
            {
                _startCellIndex = startCellIndex;
                _endCellIndex = endCellIndex;
                _startCrystalId = startCrystalId;
                _endCrystalId = endCrystalId;
            }

            public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
            {
                // Сериализация StartCellIndex (int2)
                serializer.SerializeValue(ref _startCellIndex.x);
                serializer.SerializeValue(ref _startCellIndex.y);

                // Сериализация EndCellIndex (int2)
                serializer.SerializeValue(ref _endCellIndex.x);
                serializer.SerializeValue(ref _endCellIndex.y);

                // Сериализация ulong
                serializer.SerializeValue(ref _startCrystalId);
                serializer.SerializeValue(ref _endCrystalId);
            }
        }
    }
}