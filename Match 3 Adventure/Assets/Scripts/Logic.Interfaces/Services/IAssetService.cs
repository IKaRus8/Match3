using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Logic.Interfaces.Services
{
    public interface IAssetService
    {
        UniTask<T> LoadAssetAsync<T>(string addressableKey);
        UniTask<T> LoadWithComponent<T>(string key) where T : Component;
        UniTask<GameObject> LoadGameObjectAsync(string addressableKey);
        UniTask<GameObject> LoadAndInstantiateAsync(string addressableKey, Transform parent);
        UniTask<T> LoadAndInstantiateAsync<T>(string addressableKey, Transform parent);
        
    }
}