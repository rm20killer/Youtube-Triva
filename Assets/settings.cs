using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class settings : MonoBehaviour
{
    public GameObject Menu;
    public GameObject settingButton;
    public void exitGame()
    {
        Application.Quit(0);
    }

    public void CloseMenu()
    {
        Menu.SetActive(false);
        settingButton.SetActive(true);
    }

    public void OpenMenu()
    { 
        Menu.SetActive(true);
        settingButton.SetActive(false);
    }
}
