using Unity.Mathematics;

namespace Data.Interfaces.Models.Level
{
    public interface ICell
    {
        int2 Index { get; set; }
        ICrystal Content { get; }
        bool IsEmpty { get; }
        CrystalTypeEnum CrystalType { get; }

        void SetCrystal(ICrystal crystal);
    }
}