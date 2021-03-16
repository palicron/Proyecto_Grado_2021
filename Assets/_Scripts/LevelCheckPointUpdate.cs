using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
public class LevelCheckPointUpdate : MonoBehaviour
{
    [SerializeField]
    bool OnUse = false;
    [SerializeField]
    string CheckPointName = "";
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag.Equals("Player"))
        {
            GameManager.CheckPoint = this.transform.position;
            GameManager.CheckPointName = CheckPointName;
            GameManager.CheckPointProgres++;
           
            AnalyticsResult Result = Analytics.CustomEvent("Level_Progrecion", new Dictionary<string, object>
        {
            {"Current_Level", GameManager.intance.CurrentLevelIndex},
            {"CheckPoint_Number",GameManager.CheckPointName },
            {"Level_CheckPoint_Position" , GameManager.CheckPointProgres},
            {"Play_Time_From_the_star_to_CheckPOint",Time.timeSinceLevelLoad },
            {"Play_Time_From_the_last_CheckPOint",Time.timeSinceLevelLoad - GameManager.TimeOfLastCheckPoint }
        });
            GameManager.TimeOfLastCheckPoint = Time.timeSinceLevelLoad;
            Debug.Log(Result);
            if (OnUse)
            {
                Destroy(this.gameObject);
            }
        }

    }
}
