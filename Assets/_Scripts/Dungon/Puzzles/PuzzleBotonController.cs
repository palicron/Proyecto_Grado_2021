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
    public Rigidbody MovingObject;
    public Transform MovingPositon;
    public TextMeshPro opcionText;
    [Header("Button Characteristics")]
    public float speed;
    public bool correct;
    public bool active;
    public AudioClip CorrectSound;

    // Start is called before the first frame update
    void Start()
    {
        buttonRigid = this.GetComponent<Rigidbody>();
        opcionText = transform.GetChild(0).gameObject.GetComponent<TextMeshPro>();
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

        StartCoroutine(MoveButton(1f));
        if (correct)
        {
            opcionText.SetText("Correcto");
            StartCoroutine(MoveMovingObject(1f));
            GameManager.intance.playSound(CorrectSound);
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
