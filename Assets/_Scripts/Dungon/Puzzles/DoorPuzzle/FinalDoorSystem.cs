using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FinalDoorSystem : MonoBehaviour
{

    [Header("Inicial Dependences")]
    public Rigidbody[] bodies;
    public Transform[] points;
    public GameObject MovileBody;
    public TextMeshPro Screen;
    [Header("System Values")]
    public int numberOfDoors;
    public int anwsers;
    public bool finish;
    public float speed;
    public int incorrect;



    // Start is called before the first frame update
    void Start()
    {
        finish = false;
        MovileBody = this.gameObject.transform.GetChild(0).gameObject;
        Screen = MovileBody.transform.GetChild(0).gameObject.GetComponent<TextMeshPro>();

    }

    // Update is called once per frame
    void Update()
    {
        if (anwsers == numberOfDoors)
        {
            FinishPuzzle();        
        }
    }

    void FinishPuzzle() 
    {
      
        Screen.SetText(" Completed ");
        StartCoroutine(WaitForMove(1F));
       
    }

    IEnumerator WaitForMove(float time)
    {
        Screen.SetText("Incorrect: " + incorrect  + " \n Correct: " + (anwsers-incorrect) );
        yield return new WaitForSeconds(time);
        int i = 0;
        foreach (Rigidbody bods in bodies)
        {
            bods.MovePosition(Vector3.MoveTowards(bods.position, points[i].position, speed * Time.deltaTime));
            i++;
        }
        finish = true;
    }
}
