using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FallFloorController : MonoBehaviour
{
    public GameObject MovileQuestionScreen;
    public TextMeshPro actualQuestion;
    public Rigidbody questionScreen;
    public float speed;
    public int correctAwnser;
    public string[] questions;
    public Transform[] screenPositions;
    public Rigidbody[] rigidBodies;
    public Transform[] positions;
    public bool finalQuestion;

    // Start is called before the first frame update
    void Start()
    {
        correctAwnser = 0;
        speed = 5f;
        finalQuestion = false;
        MovileQuestionScreen = GameObject.Find("QuestionScreen");
        questionScreen = MovileQuestionScreen.GetComponent<Rigidbody>();
        actualQuestion = MovileQuestionScreen.transform.GetChild(0).GetComponent<TextMeshPro>();
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
                questionScreen.MovePosition(Vector3.MoveTowards(questionScreen.position, screenPositions[correctAwnser].position, speed * Time.deltaTime));
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
            questionScreen.MovePosition(Vector3.MoveTowards(questionScreen.position, screenPositions[correctAwnser].position, speed*Time.deltaTime));
      
        }  
    }


}
