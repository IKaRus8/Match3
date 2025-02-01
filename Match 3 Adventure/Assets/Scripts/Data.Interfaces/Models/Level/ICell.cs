using Data.Enums;
using Unity.Mathematics;
using Unity.Netcode;

namespace Data.Interfaces.Models.Level
{
    public interface ICell
    {
        int2 Index { get; set; }
        ulong CrystalId { get; }
        bool IsEmpty { get; }
        CrystalTypeEnum CrystalType { get; }
        NetworkObject NetworkObject { get; }

        void SetCrystal(ICrystal crystal);
        void Select(bool value);
    }
}