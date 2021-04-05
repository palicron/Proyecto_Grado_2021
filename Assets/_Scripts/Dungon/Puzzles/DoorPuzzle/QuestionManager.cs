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
            options[i].GetComponent<TextMeshPro>().text = QaA[current].Awnsers[i];
        }
    }

    public void generateQuestion() 
    {
       
        current = Random.Range(0, QaA.Count);

        Questiontxt.text = QaA[current].question;
        SetAwnsers();
        DoorMovileCtr.correctAnwser = QaA[current].CorrectAwnser;

        QaA.RemoveAt(current);
    }
}
