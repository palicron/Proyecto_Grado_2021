using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestionManager : MonoBehaviour
{
    public DoorMovilePuzzle DoorMovileCtr;
    public TextMeshPro Questiontxt;
    public List<QandA> QaA;
    public TextMeshPro[] options;
    public int current;
    public int correctAnwser;
    public bool completed;  

     private void Start()
    {
        completed = false;
        DoorMovileCtr = GetComponent<DoorMovilePuzzle>();
    }



    void SetAwnsers() 
    {
        for (int i =0; i < options.Length; i++) 
        {
            options[i].GetComponent<TextMeshPro>().text = QaA[current].opciones[i].respuestaTexto;
            if (QaA[current].opciones[i].correct) 
            {
                DoorMovileCtr.correctAnwser = i;
            }
        }
    }

    public void generateQuestion() 
    {
       
        current = Random.Range(0, QaA.Count);

        Questiontxt.text = QaA[current].question;
        SetAwnsers();

        QaA.RemoveAt(current);
    }
}
