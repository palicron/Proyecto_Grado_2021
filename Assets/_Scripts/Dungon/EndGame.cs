using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Analytics;
public class EndGame : MonoBehaviour
{
    [SerializeField]
    CinemachineStoryboard story;
    [SerializeField]
    float TimeTotrasition = 1;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag.Equals("Player"))
        {
            Cursor.lockState = CursorLockMode.Confined;
            StartCoroutine(StarFInish());

            AnalyticsResult Result = Analytics.CustomEvent("End_game", new Dictionary<string, object>
        {
            {"Time_to_End_level",Time.timeSinceLevelLoad }    
        });
        }
    }

    IEnumerator StarFInish()
    {
        while(story.m_Alpha<1)
        {
            story.m_Alpha += 1 * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        GameManager.intance.loadLevel(6);
    }


    public void LoadMenu()
    {
        GameManager.intance.loadLevel(0);
    }
}
