using Data.Interfaces.Models.Level;

namespace Logic.Interfaces.Services.Level.Grid
{
    public interface ICrystalMoveService
    {
        void CellSelected(ICell cell);

        void SwitchCrystals(ICell startCell, ICell endCell);
    }
}