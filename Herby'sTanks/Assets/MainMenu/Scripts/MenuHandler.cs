using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour
{
    public GameObject Mainmenu;
    public GameObject PlayMenu;



    private void OnCollisionEnter(Collision collision)
    {
        if (this.gameObject.CompareTag("Play"))
        {
            Mainmenu.SetActive(false);
            PlayMenu.SetActive(true);
        }else if (this.gameObject.CompareTag("Campaign"))
        {
            SceneManager.LoadScene("SampleScene");
        }
    }
}
