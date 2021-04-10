using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

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
        AnalyticsResult Result = Analytics.CustomEvent("Level_Complete", new Dictionary<string, object>
        {
            { "Finish_Level", GameManager.intance.CurrentLevelIndex},
            {"Next_Level",nextLevel},
            {"Time_to_finish_Level",Time.timeSinceLevelLoad }
        });
        Debug.Log(Result);
        GameManager.progress[GameManager.intance.CurrentLevelIndex] = true;
        GameManager.intance.loadLevel(nextLevel, singInventory);

    }
}
