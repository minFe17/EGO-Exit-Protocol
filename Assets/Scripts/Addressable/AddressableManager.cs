using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AddressableManager : MonoBehaviour
{
    /// <summary>
    /// 지정한 주소에서 제네릭 타입 T의 에셋을 비동기 로드하고 콜백 호출
    /// </summary>
    /// <typeparam name="T">로드할 에셋 타입</typeparam>
    /// <param name="address">Addressable 에셋 주소</param>
    /// <param name="callback">로드 완료 시 호출되는 콜백</param>
    void LoadAsset<T>(string address, Action<T> callback)
    {
        Addressables.LoadAssetAsync<T>(address).Completed += handle => OnLoadDone(handle, callback);
    }

    /// <summary>
    /// Addressables 로드 완료 콜백 처리 함수
    /// </summary>
    /// <typeparam name="T">로드된 에셋 타입</typeparam>
    /// <param name="handle">비동기 작업 핸들</param>
    /// <param name="callback">로드 완료 시 호출할 콜백</param>
    void OnLoadDone<T>(AsyncOperationHandle<T> handle, Action<T> callback)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
            callback.Invoke(handle.Result);
    }

    /// <summary>
    /// TaskCompletionSource를 이용해 콜백 기반 로드 함수를 Task로 변환
    /// </summary>
    /// <typeparam name="T">로드할 에셋 타입</typeparam>
    /// <param name="address">Addressable 에셋 주소</param>
    /// <param name="completionSource">TaskCompletionSource 인스턴스</param>
    /// <returns>비동기 작업 Task</returns>
    Task LoadAssetAsync<T>(string address, TaskCompletionSource<T> completionSource)
    {
        Action<T> callback = asset => { completionSource.SetResult(asset); };
        LoadAsset(address, callback);

        return completionSource.Task;
    }

    /// <summary>
    /// 지정된 주소의 에셋을 비동기로 로드하여 반환
    /// </summary>
    /// <typeparam name="T">로드할 에셋 타입</typeparam>
    /// <param name="address">Addressable 에셋 주소</param>
    /// <returns>로드된 에셋을 담은 Task</returns>
    public async Task<T> GetAddressableAsset<T>(string address)
    {
        TaskCompletionSource<T> _loadCompletionSource = new TaskCompletionSource<T>();
        await LoadAssetAsync(address, _loadCompletionSource);
        return await _loadCompletionSource.Task;
    }

    public void Release<T>(T target)
    {
        Addressables.Release(target);
    }
}