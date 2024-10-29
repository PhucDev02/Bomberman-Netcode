using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Lobby : MonoBehaviour
{


    public static UI_Lobby Instance;
    public void Awake()
    {
        Instance = this;
        CheckRequirement();
    }

    public TextMeshProUGUI[] clientsIP;
    public TextMeshProUGUI requirement;
    public Button startBtn;
    [Button]
    public void AddClient(string name)
    {
        foreach (var client in clientsIP)
        {
            if (client.gameObject.activeInHierarchy == false)
            {
                client.gameObject.SetActive(true);
                client.text = name;
                CheckRequirement();
                return;
            }
        }
        Debug.LogError("Room is full");
    }
    [Button]
    public void RemoveClient(string name)
    {
        foreach (var client in clientsIP)
        {
            if (client.text == name)
            {
                client.gameObject.SetActive(false);
                CheckRequirement();
                return;
            }
        }
    }
    public void CheckRequirement()
    {
        int count = 0;
        foreach (var client in clientsIP)
        {
            if (client.gameObject.activeInHierarchy)
            {
                count++;
            }
        }
        if (count >= 2)
        {
            startBtn.interactable = true;
            if (count == 4)
            {
                requirement.text = "Room is full";
                requirement.gameObject.SetActive(true);
            }
            else
            {
                requirement.gameObject.SetActive(false);
            }  
        }
        else
        {
            startBtn.interactable = false;
            requirement.text = "At least 2 player to start";
            requirement.gameObject.SetActive(true);
        }
    }
}
