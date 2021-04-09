using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevel : MonoBehaviour
{
    public int nextLevel;
    public bool singInventory;


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag.Equals("Player"))
        {
            endLevel();
        }
    }

    public void endLevel()
    {
        GameManager.progress[GameManager.intance.CurrentLevelIndex] = true;
        GameManager.intance.loadLevel(nextLevel, singInventory);

    }
}
