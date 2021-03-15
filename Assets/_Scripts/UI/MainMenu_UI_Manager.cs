using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class MainMenu_UI_Manager : MonoBehaviour
{
   

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
       //@TODO implementar transicion
    }
    public void exit()
    {
        GameManager.intance.ExitGame();
    }


}
