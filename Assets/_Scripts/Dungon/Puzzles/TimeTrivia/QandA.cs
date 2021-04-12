using System.Collections;
using System.Collections.Generic;


[System.Serializable]
public class QandA
{
    public string question;
    public string[] Awnsers;
    public int CorrectAwnser;


    public List<AnswerScript> opciones;
    public QuestionType type;


    public enum QuestionType
    {
        NORMAL,
        SIMPLE,
        MULTPIPLE,
        VoF,
        COMPLETAR
    }

}
