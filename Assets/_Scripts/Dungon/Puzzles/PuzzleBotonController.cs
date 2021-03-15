using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PuzzleBotonController : MonoBehaviour
{
    [Header("Button  Dependences")]
    public Rigidbody buttonRigid;
    public Transform newPostion;
    public Rigidbody MovingObject;
    public Transform MovingPositon;
    [Header("Button Characteristics")]
    public float speed;
    public bool correct;
    public bool active;


    // Start is called before the first frame update
    void Start()
    {
        buttonRigid = this.GetComponent<Rigidbody>();
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
        buttonRigid.MovePosition(Vector3.MoveTowards(buttonRigid.position, newPostion.position, speed * Time.deltaTime));
        if (correct)
        {
            StartCoroutine(MoveMovingObject(1f));
        }

    }

    IEnumerator MoveMovingObject(float time)
    {
        yield return new WaitForSeconds(time);
        MovingObject.MovePosition(Vector3.MoveTowards(MovingObject.position, MovingPositon.position, speed * Time.deltaTime));
    }
}
