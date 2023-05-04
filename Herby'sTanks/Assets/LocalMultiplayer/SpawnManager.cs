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
    public bool shouldFinish;
    public bool joinPlayers;
    public int deadCount;

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
    public GameObject P2Tank;
    public bool hasP2Joined = false;
    public bool isP2Dead;
    //Player3
    [Header("Player 3")]
    public bool isP3Playing;
    public string P3Input;
    public GameObject P3SpawnPoint;
    public GameObject P3Prefab;
    public GameObject P3Tank;
    public bool hasP3Joined = false;
    public bool isP3Dead;
    //Player4
    [Header("Player 4")]
    public bool isP4Playing;
    public string P4Input;
    public GameObject P4SpawnPoint;
    public GameObject P4Prefab;
    public GameObject P4Tank;
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
        if(HMP.PlayerCount > 3)
        {
            isP4Playing = true;
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
        deadCount = 0;
        P1SpawnPoint = GameObject.FindWithTag("P1SpawnPoint");
        P2SpawnPoint = GameObject.FindWithTag("P2SpawnPoint");
        P3SpawnPoint = GameObject.FindWithTag("P3SpawnPoint");
        P4SpawnPoint = GameObject.FindWithTag("P4SpawnPoint");

        P1Tank = Instantiate(P1Prefab, P1SpawnPoint.transform);
        hasP1Joined = true;
        isP1Dead = false;

        if (isP2Playing)
        {
            P2Tank = Instantiate(P2Prefab, P2SpawnPoint.transform);
            hasP2Joined = true;
            isP2Dead = false;
        }
        if (isP3Playing)
        {
            P3Tank = Instantiate(P3Prefab, P3SpawnPoint.transform);
            hasP3Joined = true;
            isP3Dead = false;
        }
        if(isP4Playing)
        {
            P4Tank = Instantiate(P4Prefab, P4SpawnPoint.transform);
            hasP4Joined = true;
            isP4Dead = false;
        }
    }
    public void RemovePlayers()
    {
        if (P1Prefab.activeSelf)
        {
            DestroyImmediate(P1Tank, true);
        }
        if (P2Prefab.activeSelf)
        {
            DestroyImmediate(P2Tank, true);
        }if (P3Prefab.activeSelf)
        {
            DestroyImmediate(P3Tank, true);
        }if(P4Prefab.activeSelf)
        {
            DestroyImmediate(P4Tank, true);
        }
    }
    public void checkIfDead(int index)
    {
        switch (index)
        {
            case 1:
                isP1Dead = true;
                deadCount++;
                break;
            case 2:
                isP2Dead = true;
                deadCount++;
                break;
            case 3:
                isP3Dead = true;
                deadCount++;
                break;
            case 4:
                isP4Dead = true;
                deadCount++;
                break;
        }
        countOnDeath();
    }
    public void countOnDeath()
    {
        switch(HMP.PlayerCount)
        {
            case 0:
                break;
            case 1:
                if(deadCount == 0)
                {
                    shouldFinish = true;
                }
                break;
            case 2:
                if(deadCount == 1)
                {
                    shouldFinish = true;
                    deadCount = 0;
                }
                break;
            case 3:
                if (deadCount == 2)
                {
                    shouldFinish = true;
                }
                break;
            case 4:
                if (deadCount == 3)
                {
                    shouldFinish = true;
                }
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
