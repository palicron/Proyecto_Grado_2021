using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriviaQnA : MonoBehaviour
{
    public TriviaManager Oldmanager;
    public TriviaManager NewManger;



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            NewManger.QaA = Oldmanager.QaA;
            NewManger.changeQuestion();
        }

    }
}
