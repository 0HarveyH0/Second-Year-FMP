using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using TMPro;

public class SpawnManager : MonoBehaviour
{
    public PlayerInputManager PIM;
    public HowManyPlayers HMP;
    public bool joinPlayers;

    //Player1
    [Header("Player 1")]
    public string P1Input;
    public GameObject P1SpawnPoint;
    public GameObject P1Prefab;
    public GameObject P1Tank;
    public bool hasP1Joined = false;
    public bool isP1Dead;
    //Player2
    [Header("Player 2")]
    public bool isP2Playing;
    public string P2Input;
    public GameObject P2SpawnPoint;
    public GameObject P2Prefab;
    public bool hasP2Joined = false;
    public bool isP2Dead;
    //Player3
    [Header("Player 3")]
    public bool isP3Playing;
    public string P3Input;
    public GameObject P3SpawnPoint;
    public GameObject P3Prefab;
    public bool hasP3Joined = false;
    public bool isP3Dead;
    //Player4
    [Header("Player 4")]
    public bool isP4Playing;
    public string P4Input;
    public GameObject P4SpawnPoint;
    public GameObject P4Prefab;
    public bool hasP4Joined = false;
    public bool isP4Dead;

    private void Start()
    {
        HMP = GameObject.FindWithTag("HowManyPlayers").GetComponent<HowManyPlayers>();
        if(HMP.PlayerCount > 1)
        {
            isP2Playing = true;
        }
        if(HMP.PlayerCount > 2)
        {
            isP3Playing = true;
        }
    }

    public void Update()
    {
        if (joinPlayers)
        {
            JoinPlayers();
        }

    }

    public void JoinPlayers()
    {
        P1SpawnPoint = GameObject.FindWithTag("P1SpawnPoint");
        P2SpawnPoint = GameObject.FindWithTag("P2SpawnPoint");
        P3SpawnPoint = GameObject.FindWithTag("P3SpawnPoint");

            P1Tank = Instantiate(P1Prefab, P1SpawnPoint.transform);
            hasP1Joined = true;
        
        if (isP2Playing)
        {
            Instantiate(P2Prefab, P2SpawnPoint.transform);
            hasP2Joined = true;
        }
        if (isP3Playing)
        {
            Instantiate(P3Prefab, P3SpawnPoint.transform);
            hasP3Joined = true;
        }
    }

    public void RemovePlayers()
    {
        if (P1Prefab.activeSelf)
        {
            DestroyImmediate(P1Tank, true);
        }else if (P2Prefab.activeSelf)
        {
            DestroyImmediate(P2Prefab, true);
        }
    }

    public void checkIfDead(int index)
    {
        switch (index)
        {
            case 1:
                isP1Dead = true;
                break;
            case 2:
                isP2Dead = true;
                break;
            case 3:
                isP3Dead = true;
                break;
            case 4:
                isP4Dead = true;
                break;
        }
    }

    public void OnPlayerJoined(PlayerInput playerInput)
    {
        switch (playerInput.playerIndex)
        {
            case 0:
                P1Input = playerInput.currentControlScheme;
                break;
            case 1:
                isP2Playing = true;
                P2Input = playerInput.currentControlScheme;
                break;
        }


        
        Debug.Log("PlayerInput ID: " + playerInput.playerIndex);
        /*
        playerInput.gameObject.GetComponent<PlayerDetails>().playerID = playerInput.playerIndex + 1;
        playerInput.gameObject.GetComponent<PlayerDetails>().playerDevice = playerInput.currentControlScheme;
        */
    }
}
