using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUB_Controler : MonoBehaviour
{
    public static HUB_Controler intance;
    public static HUB_Controler Instance { get { if (intance == null) { intance = new HUB_Controler(); } return intance; } }
    // Start is called before the first frame update
    [SerializeField]
    GameObject TomsTrigger;
    [SerializeField]
    GameObject IndutryDorr;
    [SerializeField]
    GameObject PaperColum;
    [SerializeField]
    GameObject IndustriColum;
    [SerializeField]
    GameObject TowerDoor;

    private void Start()
    {
        intance = this;
    }

    public void starHub()
    {
        Debug.Log("Hub");
        Debug.Log(GameManager.progress[1]);
        if (GameManager.progress[1])
        {
            Destroy(TomsTrigger);
        }
    }
}
