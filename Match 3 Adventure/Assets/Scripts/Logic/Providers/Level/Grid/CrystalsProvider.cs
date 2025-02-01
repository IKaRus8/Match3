using Cysharp.Threading.Tasks;
using Data.Enums;
using Data.Interfaces.Models.Level;
using Data.ScriptableObjects;
using Extensions;
using Logic.Interfaces.Providers.Level.Grid;
using Logic.Interfaces.Services;
using Unity.Netcode;
using UnityEngine;

namespace Logic.Providers.Level.Grid
{
    public class CrystalsProvider : ICrystalsProvider
    {
        private const string CrystalsConfigKey = "CrystalsConfig";
        
        private readonly IAssetService _assetService;
        
        private CrystalsConfig _crystalsConfig;

        public CrystalsProvider(IAssetService assetService)
        {
            _assetService = assetService;
            
            LoadCrystalsConfig().Forget();
        }

        public NetworkObject GetRandomCrystalPrefab()
        {
            var randomType = CrystalTypeEnum.None.GetRandom();

            return GetCrystalPrefab(randomType);
        }

        public NetworkObject GetCrystalPrefab(CrystalTypeEnum crystalType)
        {
            return _crystalsConfig.GetCrystal(crystalType);
        }

        public ICrystal GetSpawnedCrystal(ulong id)
        {
            if (NetworkManager.Singleton.SpawnManager.SpawnedObjects.TryGetValue(id, out var networkObject))
            {
                return networkObject.GetComponent<ICrystal>();
            }
            
            Debug.LogError($"Crystal {id} not found");
            
            return null;
        }

        private async UniTask LoadCrystalsConfig()
        {
            _crystalsConfig = await _assetService.LoadAssetAsync<CrystalsConfig>(CrystalsConfigKey);
        }
    }
}