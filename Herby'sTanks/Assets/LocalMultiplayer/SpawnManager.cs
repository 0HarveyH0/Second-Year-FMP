using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpawnManager : MonoBehaviour
{
    public void OnPlayerJoined(PlayerInput playerInput)
    {
        Debug.Log("PlayerInput ID: " + playerInput.playerIndex);
        playerInput.gameObject.GetComponent<PlayerDetails>().playerID = playerInput.playerIndex + 1;
    }
}
