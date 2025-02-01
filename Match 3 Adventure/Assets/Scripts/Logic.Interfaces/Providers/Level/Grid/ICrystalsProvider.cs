using Data.Enums;
using Data.Interfaces.Models.Level;
using Unity.Netcode;

namespace Logic.Interfaces.Providers.Level.Grid
{
    public interface ICrystalsProvider
    {
        NetworkObject GetRandomCrystalPrefab();
        NetworkObject GetCrystalPrefab(CrystalTypeEnum crystalType);
        ICrystal GetSpawnedCrystal(ulong id);
    }
}