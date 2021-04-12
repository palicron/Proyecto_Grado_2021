using System.Collections;
using System.Collections.Generic;


[System.Serializable]
public class QandA
{
    public string question;
    public string hint;
    public List<AnswerScript> opciones;
    public QuestionType type;



    public enum QuestionType
    {
        NORMAL,
        SIMPLEANSWER,
        MULTPIPLEANSWERS,
        VF,
        COMPLETAR,
        ORDENAR
    }


}
