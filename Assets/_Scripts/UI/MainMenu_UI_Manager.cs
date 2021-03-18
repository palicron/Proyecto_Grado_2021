using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class MainMenu_UI_Manager : MonoBehaviour
{

    public GameObject info_ui;

    public void loandTutorial()
    {
        GameManager.intance.loadLevel(1);
    }

    public void loadSpecLevel(int index)
    {
        GameManager.intance.loadLevel(index);
    }

   public void infoButton()
    {
        info_ui.SetActive(true);
    }
    public void exit()
    {
        GameManager.intance.ExitGame();
    }


}
