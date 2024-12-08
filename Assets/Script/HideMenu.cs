using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideMenu : MonoBehaviour
{
    public GameObject APIgameObject;
    public GameObject StreamID;
    public GameObject TextDoNotShow;
    public GameObject ButtonDonotShow;
    public GameObject ButtonToStart;

    public void hide()
    {
        APIgameObject.SetActive(false);
        StreamID.SetActive(false);
        TextDoNotShow.SetActive(false);
        ButtonDonotShow.SetActive(false);
        ButtonToStart.SetActive(true);
    }
}
