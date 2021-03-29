using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingPuzzle : MonoBehaviour
{
    [Header("Platform  Dependences")]
    public Rigidbody platformRB;
    public Transform newPostion;
    [Header("Platform Characteristics")]
    public float speed;
    public bool correct;
    public bool active;
    public bool volverAlInicial;
    public Vector3 initialPos;
    public AudioClip CorrectSound;
    // Start is called before the first frame update
    void Start()
    {
        platformRB = this.GetComponent<Rigidbody>();
        volverAlInicial = false;
    }


    // Update is called once per frame
    void Update()
    {
        if (active && !correct)
        {
            MovePlatform();
        }
       //else if(active && correct)
       // {
       //    GameManager.intance.playSound(CorrectSound);
       //}
        else if (volverAlInicial) 
        {
            platformRB.MovePosition(Vector3.MoveTowards(platformRB.position, initialPos, speed * Time.deltaTime));
            volverAlInicial = false;
        }
    }


    void MovePlatform()
    {
        platformRB.MovePosition(Vector3.MoveTowards(platformRB.position, newPostion.position, speed * Time.deltaTime));
        StartCoroutine(MoveMovingObject(3f));
    }


    IEnumerator MoveMovingObject(float time)
    {
        yield return new WaitForSeconds(time);
        active = false;
        volverAlInicial = true;
    }
}
