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
    [SerializeField]
    private void Start()
    {
        intance = this;
    }

    public void starHub()
    {
       
        if (GameManager.progress[1])
        {
            Destroy(TomsTrigger);
        }
        if(GameManager.progress[2])
        {
            Destroy(TomsTrigger);
            Destroy(IndutryDorr);
        }
        if(GameManager.progress[3])
        {
            Destroy(TomsTrigger);
            Destroy(IndutryDorr);
            Destroy(TowerDoor);
        }
    }
}
