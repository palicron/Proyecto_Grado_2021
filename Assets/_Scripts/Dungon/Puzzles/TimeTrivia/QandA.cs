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



    public string shuffle(string palabra) 
    {
        char[] characters = palabra.ToCharArray();
        System.Random randomRange = new System.Random();
        int numberChar = characters.Length;
        while (numberChar>1) 
        {
            numberChar--;
            int index = randomRange.Next(numberChar+1);
            var value = characters[index];
            characters[index]= characters[numberChar];
            characters[numberChar] = value;

        }
        return new string(characters);
    
    }
}
