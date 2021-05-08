using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalLevelCtr : MonoBehaviour
{

    public static FinalLevelCtr intance;
    public static FinalLevelCtr Instance { get { if (intance == null) { intance = new FinalLevelCtr(); } return intance; } }
    // Start is called before the first frame update

    [SerializeField]
    GameObject MovingParts;
    [SerializeField]
    GameObject MideelPise;
    [SerializeField]
    Vector3 post1;
    [SerializeField]
    Vector3 post2;
    [SerializeField]
    float ScrollingSpeed;
    [SerializeField]
    float EventSpeed = 1;
    [SerializeField]
    GameObject NormalCamera;
    [SerializeField]
    GameObject NewCamera;
    [SerializeField]
    RecyclingBinsPuzzle Puzzel;
    Material scrollingMat;
    
    void Start()
    {
        intance = this;
        scrollingMat = MideelPise.GetComponent<Renderer>().material;
        Debug.Log(MideelPise.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void starFight()
    {
        NewCamera.SetActive(true);
        NormalCamera.SetActive(false);
        StartCoroutine(StarMove());
    }

    public void endFight()
    {
        NormalCamera.SetActive(true);
        NewCamera.SetActive(false);
        StartCoroutine(EndMoveMove());
    }
    IEnumerator StarMove()
    {
        while(Vector3.Distance(MideelPise.transform.position, post1)>36.0f)
        {
            Debug.Log(Vector3.Distance(MideelPise.transform.position, post1));
            MovingParts.transform.position = Vector3.MoveTowards(MovingParts.transform.position, post1, EventSpeed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        
        }
        scrollingMat.SetVector("Vector2_9793e25384094873bf5a133e32e2768a", new Vector2(0, ScrollingSpeed));

        Puzzel.StartPuzzle();

    }
    IEnumerator EndMoveMove()
    {
        while (Vector3.Distance(MideelPise.transform.position, post2) > 0.5f)
        {

            MovingParts.transform.position = Vector3.MoveTowards(MovingParts.transform.position, post2, EventSpeed* Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }

     
    }

}
