using Cysharp.Threading.Tasks;
using Data.Enums;

namespace Data.Interfaces.Models.Level
{
    public interface ICrystal
    {
        ulong CrystalId { get; }
        CrystalTypeEnum CrystalType { get; }

        UniTask MoveToCell(ICell cell);
    }
}