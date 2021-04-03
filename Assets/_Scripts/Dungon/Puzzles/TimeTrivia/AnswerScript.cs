using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerScript : MonoBehaviour
{
    public bool isCorrect=false;
    public TriviaManager quizManager;
    void Awnser()
    {
        if (isCorrect) 
        {
            Debug.Log("Correct");
        }
        if (!isCorrect)
        {
            Debug.Log("Incorrect");
        }
    }
}
