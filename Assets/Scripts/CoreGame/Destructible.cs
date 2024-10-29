using Cysharp.Threading.Tasks;
using System.Reflection;
using System.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    public float destructionTime = 1f;

    [Range(0f, 1f)]
    public float itemSpawnChance = 0.2f;
    public GameObject[] spawnableItems;

    private void Start()
    {
        //Destroy(gameObject, destructionTime) ;
        //DestroyAsync();
        DestroyAsyncServerRpc();
    }
    [ServerRpc]
    public async void DestroyAsyncServerRpc()
    {
        if (!GameController.Instance.IsServer) return;
        await UniTask.Delay((int)destructionTime * 1000);
        NetworkObjectSpawner.DestroyNetworkObject(gameObject);
    }
    private void OnDestroy()
    {
        if (!GameController.Instance.IsServer) return;
        if (spawnableItems.Length > 0 && Random.value < itemSpawnChance)
        {
            RequestSpawnBoosterServerRpc();
        }
    }
    [ServerRpc]
    public void RequestSpawnBoosterServerRpc()
    {
        int randomIndex = Random.Range(0, spawnableItems.Length);
        NetworkObjectSpawner.SpawnNewNetworkObject(spawnableItems[randomIndex], transform.position, Quaternion.identity);
    }

}
