using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerQuestion : MonoBehaviour
{
   public QuestionManager OldManager;

   public QuestionManager newManager; 


      private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if(OldManager!=null && newManager!=null)
            {
                newManager.QaA = OldManager.QaA;
            }
           newManager.generateQuestion();
        }

    }

}
