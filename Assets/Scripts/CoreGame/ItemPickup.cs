using Unity.Netcode;
using UnityEngine;
public class ItemPickup : NetworkBehaviour
{
    public enum ItemType
    {
        ExtraBomb,
        BlastRadius,
        SpeedIncrease,
    }
    public ItemType type;
    private void OnItemPickUp(GameObject player)
    {
        if (!GameController.Instance.IsServer) return;
        switch (type)
        {
            case ItemType.ExtraBomb:
                player.GetComponent<BombController>().AddBomb();
                break;
            case ItemType.BlastRadius:
                player.GetComponent<BombController>().explosionRadius++;
                break;
            case ItemType.SpeedIncrease:
                player.GetComponent<MovementController>().speed++;
                break;
        }
        NetworkObjectSpawner.DestroyNetworkObject(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            OnItemPickUp(other.gameObject);
        }
    }
}