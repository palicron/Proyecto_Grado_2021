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
    public Vector3 initialPos;
    public AudioClip CorrectSound;
    // Start is called before the first frame update
    void Start()
    {
        platformRB = this.GetComponent<Rigidbody>();
        initialPos = new Vector3(platformRB.position.x, platformRB.position.y, platformRB.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (active && !correct)
        {
            MovePlatform();
        }
        else if (!active) { platformRB.MovePosition(Vector3.MoveTowards(platformRB.position, initialPos, speed * Time.deltaTime)); }
        else if(active && correct)
        {
            GameManager.intance.playSound(CorrectSound);
        }
    }


    void MovePlatform()
    {
        StartCoroutine(MoveMovingObject(1f));
    }


    IEnumerator MoveMovingObject(float time)
    {
        yield return new WaitForSeconds(time);
        platformRB.MovePosition(Vector3.MoveTowards(platformRB.position, newPostion.position, speed * Time.deltaTime));

    }
}
