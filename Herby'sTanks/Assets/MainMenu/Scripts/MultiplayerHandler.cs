using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MultiplayerHandler : MonoBehaviour
{
    //Player Input Manager
    public PlayerInputManager InputManager;
    public HowManyPlayers HMP;
    
    //Level Transitions
    [Header("Level Transitions")]
    public bool shouldTransition;
    

    private void Update()
    {
        if (shouldTransition)
        {
            SceneManager.LoadScene("SampleScene");
            shouldTransition = false;
        }

    }
    public void OnPlayerJoined(PlayerInput playerInput)
    {
        HMP.PlayerCount++;
    }

}
