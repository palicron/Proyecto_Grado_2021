using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class PuzzleBotonController : MonoBehaviour
{
    [Header("Button  Dependences")]
    public Rigidbody buttonRigid;
    public Transform newPostion;
    [Header("Button  Correct Variables")]
    public Rigidbody MovingObject;
    public Transform MovingPositon;
    public TextMeshPro opcionText;
    [Header("Button Characteristics")]
    public float speed;
    public bool correct;
    public bool isNotCorrect;
    public bool active;
    //public AudioClip CorrectSound;

    // Start is called before the first frame update
    void Start()
    {
        buttonRigid = this.GetComponent<Rigidbody>();
        opcionText = transform.GetChild(0).gameObject.GetComponent<TextMeshPro>();
        if (!correct) 
        {
            isNotCorrect = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            MoveButton();
        }
    }

    void MoveButton()
    {

      
        if (correct)
        {
            StartCoroutine(MoveButton(1f));
            opcionText.SetText("R");
            StartCoroutine(MoveMovingObject(1f));
            //  GameManager.intance.playSound(CorrectSound);
        }
        else
        {
            StartCoroutine(MoveButton(0f));
            opcionText.SetText("X");
        }

    }

    IEnumerator MoveMovingObject(float time)
    {
        yield return new WaitForSeconds(time);
        MovingObject.MovePosition(Vector3.MoveTowards(MovingObject.position, MovingPositon.position, speed * Time.deltaTime));
    }

    IEnumerator MoveButton(float time)
    {
        yield return new WaitForSeconds(time);
        buttonRigid.MovePosition(Vector3.MoveTowards(buttonRigid.position, newPostion.position, speed * Time.deltaTime));
    }
}
