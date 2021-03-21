using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePlatform : MonoBehaviour
{

    [Header("Platform  Dependences")]
    public Rigidbody platformRB;
    public Transform newPostion;
    public FallFloorController FallFloorCtr;
    [Header("Platform Characteristics")]
    public float speed;
    public bool correct;
    public bool active;
    public bool AlreadyActivated;
    public bool volverInicio;
    public Vector3 initialPos;


    // Start is called before the first frame update
    void Start()
    {
        platformRB = this.GetComponent<Rigidbody>();
        AlreadyActivated = false;
        initialPos = new Vector3(platformRB.position.x, platformRB.position.y, platformRB.position.z);
        FallFloorCtr=transform.GetComponentInParent<FallFloorController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (active && !correct)
        {
            MovePlatform();
        }
        else if (!active) { platformRB.MovePosition(Vector3.MoveTowards(platformRB.position, initialPos, speed * Time.deltaTime)); }
    }


    void MovePlatform()
    {
        platformRB.MovePosition(Vector3.MoveTowards(platformRB.position, newPostion.position, speed * Time.deltaTime));
        StartCoroutine(MoveMovingObject(2f)); 
        
    }


    IEnumerator MoveMovingObject(float time)
    {
        yield return new WaitForSeconds(time);
        active = false;
    }
}
