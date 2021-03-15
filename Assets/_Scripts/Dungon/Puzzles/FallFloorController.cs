using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FallFloorController : MonoBehaviour
{
    public GameObject QuestionField;
    public TextMeshPro actualQuestion;
    public float speed;
    public int correctAwnser;
    public string[] questions;
    public Rigidbody[] rigidBodies;
    public Transform[] positions;
    public bool finalQuestion;

    // Start is called before the first frame update
    void Start()
    {
        correctAwnser = 0;
        speed = 5f;
        QuestionField = transform.GetChild(0).gameObject;
        actualQuestion = QuestionField.GetComponent<TextMeshPro>();
        finalQuestion = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!finalQuestion)
        {
            ChangeQuestion(correctAwnser);
        }
        else if(finalQuestion)
        {
            int i=0;
            foreach (Rigidbody rb in rigidBodies)
            {
              rb.MovePosition(Vector3.MoveTowards(rb.position, positions[i].position, speed* Time.deltaTime));  
              i++; 
            }
        }
      
    }

    void ChangeQuestion(int numberQuestion)
    {
        if (questions.Length == correctAwnser)
        {
            finalQuestion = true;
            actualQuestion.SetText("Completed");
        }
        else if (questions[numberQuestion]!=null)
        {
            actualQuestion.SetText(questions[numberQuestion]); 
      
        }  
    }


}
