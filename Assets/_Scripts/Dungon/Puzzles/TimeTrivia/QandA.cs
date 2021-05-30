using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QandA
{
    public string question;
    public List<AnswerScript> opciones;
    public QuestionType type;

    public enum QuestionType
    {
        NORMAL,
        SIMPLEANSWER,
    }
}
