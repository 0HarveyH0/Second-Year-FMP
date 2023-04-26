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
        if (collision.collider.CompareTag("Bullet"))
        {
            if (this.gameObject.CompareTag("Play"))
            {
                Mainmenu.SetActive(false);
                PlayMenu.SetActive(true);
            }
            else if (this.gameObject.CompareTag("Campaign"))
            {
                SceneManager.LoadScene("Campaign");
            }else if (this.gameObject.CompareTag("Quit"))
            {
                Application.Quit();
            }else if (this.gameObject.CompareTag("Back"))
            {
                if(PlayMenu.activeSelf)
                {
                    PlayMenu.SetActive(false);
                }
                Mainmenu.SetActive(true);
            }
        }
    }
}
