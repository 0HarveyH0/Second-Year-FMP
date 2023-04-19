using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DropdownHandler : MonoBehaviour
{
    public SpawnManager spawnManager;


    public TMP_Dropdown p1_dropdown;
    public TMP_Dropdown p2_dropdown;
    public TMP_Dropdown p3_dropdown;
    public TMP_Dropdown p4_dropdown;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void P1OnDropdownValueChanged(int value)
    {
        Debug.Log(p1_dropdown.options[value].text);
    }

}
