using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDetails : MonoBehaviour
{
    public int playerID;
    public string playerDevice;
    // Start is called before the first frame update
    void Start()
    {
        playerDevice = GetComponent<PlayerInput>().currentControlScheme;
        playerID = GetComponent<PlayerInput>().playerIndex;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
