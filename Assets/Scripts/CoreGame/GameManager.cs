using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private void Awake()
    {
        Instance = this;
    }
    public List<GameObject> players;
    public void AddPlayer(GameObject gameObject)
    {
        players.Add(gameObject);
    }
    public void FireDieEvent(int playerId)
    {
        if (playerId == (int)PlayerController.ownerID)
            Debug.Log("You lost");
        int alive = 0;
        foreach (GameObject gameObject in players)
        {
            if (gameObject.activeInHierarchy)
            {
                alive++;
            }
        }
        if (alive == 1)
        {
            foreach (GameObject gameObject in players)
            {
                if (gameObject.activeInHierarchy)
                {
                    if (gameObject.GetComponent<PlayerController>().idInGame == (int)PlayerController.ownerID)
                        Debug.Log("You Win");
                    UI_Manager.Instance.ShowGameOver(gameObject.GetComponent<PlayerController>().idInGame);
                }
            }
        }
    }
}