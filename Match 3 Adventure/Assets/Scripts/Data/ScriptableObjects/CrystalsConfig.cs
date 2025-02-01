using Data.Enums;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Rendering;

namespace Data.ScriptableObjects
{
    [CreateAssetMenu(fileName = "CrystalsConfig", menuName = "Configs/CrystalsConfig")]
    public class CrystalsConfig : ScriptableObject
    {
        public SerializedDictionary<CrystalTypeEnum, NetworkObject> _crystals;

        public NetworkObject GetCrystal(CrystalTypeEnum crystalType)
        {
            return _crystals[crystalType];
        }
    }
}