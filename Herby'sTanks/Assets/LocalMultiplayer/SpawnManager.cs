using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;


public class SpawnManager : MonoBehaviour
{
    public PlayerInputManager PIM;

    [Header("Level Transitions")]
    public bool levelTransitioned;
    public bool shouldTransition;
    public string initialScene;

    //Player1
    [Header("Player 1")]
    public string P1Input;
    public GameObject P1SpawnPoint;
    public GameObject P1Prefab;
    bool hasP1Joined = false;
    //Player2
    [Header("Player 2")]
    public bool isP2Playing;
    public string P2Input;
    public GameObject P2SpawnPoint;
    public GameObject P2Prefab;
    bool hasP2Joined = false;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        initialScene = SceneManager.GetActiveScene().name;

    }

    public void Update()
    {
        if (shouldTransition)
        {
            SceneManager.LoadScene("SampleScene");
            shouldTransition = false;
        }
        if (SceneManager.GetActiveScene().name != initialScene)
        {
            levelTransitioned = true;           
            OnTransition();            
        }

    }

    public void OnTransition()
    {
        PIM.joinBehavior = PlayerJoinBehavior.JoinPlayersManually;
        P1SpawnPoint = GameObject.FindWithTag("P1SpawnPoint");
        P2SpawnPoint = GameObject.FindWithTag("P2SpawnPoint");
        if(!hasP1Joined)
        {
            Instantiate(P1Prefab, P1SpawnPoint.transform);
            hasP1Joined = true;
        }
        if (!hasP2Joined && isP2Playing)
        {
            Instantiate(P2Prefab, P2SpawnPoint.transform);
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
        playerInput.gameObject.GetComponent<PlayerDetails>().playerID = playerInput.playerIndex + 1;
        playerInput.gameObject.GetComponent<PlayerDetails>().playerDevice = playerInput.currentControlScheme;

    }

    public void onLevelTransition()
    {
        levelTransitioned = true;
    }
}
