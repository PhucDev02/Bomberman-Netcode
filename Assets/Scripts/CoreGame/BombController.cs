using System.Collections;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BombController : NetworkBehaviour
{
    [Header("Bomb")]
    public KeyCode inputKey = KeyCode.Space;
    public GameObject bombPrefab;
    public float bombFuseTime = 3f;
    public int bombAmount = 1;
    private int bombsRemaining;

    [Header("Explosion")]
    public GameObject explosionPrefab;
    public LayerMask explosionLayerMask;
    public float explosionDuration = 1f;
    public int explosionRadius = 1;

    [Header("Destructible")]
    public Tilemap destructibleTiles;
    public GameObject destructiblePrefab;

    private void OnEnable()
    {
        bombsRemaining = bombAmount;
        destructibleTiles = GameObject.Find("Destructibles").GetComponent<Tilemap>();
    }

    private void Update()
    {
        if (!IsOwner || UI_Manager.Instance.IsPause()) return;
        if (bombsRemaining > 0 && Input.GetKeyDown(inputKey))
        {
            RequestPlaceBombServerRpc();
        }
    }
    [ServerRpc]
    public void RequestPlaceBombServerRpc()
    {
        StartCoroutine(PlaceBomb());
    }

    public void PutBomb()
    {
        if (bombsRemaining > 0)
        {
            StartCoroutine(PlaceBomb());
        }
    }
    private IEnumerator PlaceBomb()
    {
        Vector2 position = transform.position;
        position.x = Mathf.Round(position.x);
        position.y = Mathf.Round(position.y);

        GameObject bomb = NetworkObjectSpawner.SpawnNewNetworkObject(bombPrefab, position, Quaternion.identity);
        bombsRemaining--;

        yield return new WaitForSeconds(bombFuseTime);

        //if bomb is pushed
        position = bomb.transform.position;
        position.x = Mathf.Round(position.x);
        position.y = Mathf.Round(position.y);

        GameObject obj = NetworkObjectSpawner.SpawnNewNetworkObject(explosionPrefab, position, Quaternion.identity);
        Explosion explosion = obj.GetComponent<Explosion>();
        explosion.SetActiveRenderer(explosion.start);
        explosion.DestroyAfter(explosionDuration);

        Explode(position, Vector2.up, explosionRadius);
        Explode(position, Vector2.down, explosionRadius);
        Explode(position, Vector2.left, explosionRadius);
        Explode(position, Vector2.right, explosionRadius);

        Destroy(bomb.gameObject);
        bombsRemaining++;
    }

    private void Explode(Vector2 position, Vector2 direction, int length)
    {
        if (length <= 0)
        {
            return;
        }

        position += direction;

        if (Physics2D.OverlapBox(position, Vector2.one / 2f, 0f, explosionLayerMask))
        {
            ClearDestructibleClientRpc(position);
            return;
        }

        GameObject obj = NetworkObjectSpawner.SpawnNewNetworkObject(explosionPrefab, position, Quaternion.identity);
        Explosion explosion = obj.GetComponent<Explosion>();
        explosion.SetActiveRenderer(length > 1 ? explosion.middle : explosion.end);
        explosion.SetDirection(direction);
        explosion.DestroyAfter(explosionDuration);

        Explode(position, direction, length - 1);
    }

    [ClientRpc]
    private void ClearDestructibleClientRpc(Vector2 position)
    {
        Vector3Int cell = destructibleTiles.WorldToCell(position);
        TileBase tile = destructibleTiles.GetTile(cell);

        if (tile != null)
        {
            if (GameController.Instance.IsServer)
                NetworkObjectSpawner.SpawnNewNetworkObject(destructiblePrefab, position, Quaternion.identity);
            destructibleTiles.SetTile(cell, null);
        }
    }
    public void AddBomb()
    {
        bombAmount++;
        bombsRemaining++;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Bomb"))
        {
            other.isTrigger = false;
        }
    }

}
