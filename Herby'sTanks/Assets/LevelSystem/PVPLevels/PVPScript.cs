
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PVPScript : MonoBehaviour
{
    private TankScript tankScript;
    public bool isShot;



    // Start is called before the first frame update
    void Start()
    {
        tankScript= GetComponent<TankScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if(tankScript.isShot)
        {
            isShot= true;
        }
    }
}
