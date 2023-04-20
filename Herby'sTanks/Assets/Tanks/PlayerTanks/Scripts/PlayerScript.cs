using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum PlayerStates
{
    Menus,
    Tank
}

public class PlayerScript : MonoBehaviour
{
    private GameObject tank;
    public GameObject tankPrefab;
    private PlayerStates states;

    void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {
        switch(states)
        {
            case PlayerStates.Menus:
                Menus();
                break;
            case PlayerStates.Tank:
                if(tank == null)
                {
                    SpawnTank();
                }
                break;
        }
    }
    public void SpawnTank()
    {
        tank.SetActive(true);
        
    }
    public void Menus()
    {
        if (tank != null)
        {
            DestoryTank();
        }
    }
    public void DestoryTank()
    {
        tank.SetActive(false);
    }
}
