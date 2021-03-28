using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    [Header("Door Dependences")]
    public Rigidbody DoorRB;
    public Transform newPosition;
    public DoorMovilePuzzle oldDoorScript;

    [Header("Door Characteristics")]

    public float speed;
  
    public int IncorrectAccountant;
    public bool isChosed;
    public bool correct;
   
    // Start is called before the first frame update
    void Start()
    {
        if(oldDoorScript!=null){ IncorrectAccountant = oldDoorScript.incorrect;}
        DoorRB = this.gameObject.GetComponent<Rigidbody>();
        isChosed = false;
        speed= 10f;
    }

    // Update is called once per frame
    void Update()
    {
          if (isChosed)
        {
            if(correct){ MoveDoor();}
            else if(!correct){StartCoroutine(WaitForMove(0.3f)); }
           
        }
    }

    void MoveDoor()
    {
        DoorRB.MovePosition(Vector3.MoveTowards(DoorRB.position, newPosition.position, speed * Time.deltaTime));
    }


     IEnumerator WaitForMove(float time) {
        yield return new WaitForSeconds(time * (IncorrectAccountant+1));
        MoveDoor();
    }
}
