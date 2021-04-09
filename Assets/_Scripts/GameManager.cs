using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Analytics;
public class GameManager : MonoBehaviour
{
    public static GameManager intance;
    public static GameManager Instance { get { if (intance == null) { intance = new GameManager(); } return intance; } }
    // Start is called before the first frame update

    public static Vector3 CheckPoint = Vector3.zero;

    public static bool[] progress = new bool[]
       {false,false,false,false,false};

    public static string CheckPointName = "";
    public static int CheckPointProgres = 0;
    public static float TimeOfLastCheckPoint = 0;


    [SerializeField]
    string[] Levels;

    public  int CurrentLevelIndex;
    public PlayerCtr Player;

    [SerializeField]
    public EquipmentManager Equipment;
    [SerializeField]

    float YkillZone;
    [SerializeField]
    int YKillZoneDmg = 10;
    public bool StarLoad = false;

    float GameStarTime = 0.0f;

    AudioSource audio;
    [SerializeField]
    AudioClip pushSound;
    private void Awake()
    {
        intance = this;
       
        DontDestroyOnLoad(this.gameObject);
        audio = GetComponent<AudioSource>();
        audio.volume = 0.35f;

    }
    void Start()
    {

        //Cursor.lockState = CursorLockMode.Confined;

        //recordad que esto solo e spara le nivel de prueba tiene que estar es cuando se carga un lvl
        iniComponents();
        if (StarLoad)
         {
           loadLevel(0);
         }


    }

    // Update is called once per frame
    void Update()
    {
       
        if (Player && Player.transform.position.y <= YkillZone)
        {
            resetPlayer(YKillZoneDmg);
        }
    }






    public void PauseGame()
    {
        if (UIManager.GameIsPaused)
        {
            UIManager.Instance.Resume();
            audio.PlayOneShot(pushSound);
            
        }
        else
        {
            UIManager.Instance.Pause();
            audio.PlayOneShot(pushSound);
         
        }
    }

    void iniComponents()
    {

        if (Player)
        {
            healthsystems hs = Player.GetComponent<healthsystems>();
            hs.healthUpdate += UIManager.Instance.UpdatePlayerLife;
            hs.deathNotify += PlayerDeath;
            hs.Init();
            UIManager.Instance.UpdatePlayerLife(hs.getHealthPorcentage());

        }
        else
        {
            GameObject playerObjet = GameObject.FindGameObjectWithTag("Player");
            if (!playerObjet)
                return;
            Player = playerObjet.GetComponent<PlayerCtr>(); 
            Player.GetComponent<healthsystems>().healthUpdate += UIManager.Instance.UpdatePlayerLife;
            healthsystems hs = Player.GetComponent<healthsystems>();
            hs.healthUpdate += UIManager.Instance.UpdatePlayerLife;
            hs.deathNotify += PlayerDeath;
            hs.Init();
            UIManager.Instance.UpdatePlayerLife(hs.getHealthPorcentage());
        }
       

    }

    public void loadLevel(int index,bool TransferInv=false)
    {

        CurrentLevelIndex = index;
        GameManager.CheckPointProgres = 0;
        StartCoroutine(LoadYourAsyncScene(index, TransferInv));
    }
    public void ExitGame()
    {

        Application.Quit();
    }


    public void resetPlayer(int Dmg =0)
    {
        Player.transform.position = GameManager.CheckPoint;
        if(Dmg != 0)
        {
            Player.GetComponent<healthsystems>().TakeDmg(Dmg);
        }
    }

    public void PlayerDeath()
    {
        AnalyticsResult Result = Analytics.CustomEvent("Player_Death", new Dictionary<string, object>
        {
            { "Current_Level", CurrentLevelIndex},
            {"Level_CheckPoint_Name",GameManager.CheckPointName },
            {"Level_CheckPoint_Position" , GameManager.CheckPointProgres},
            {"Play_Time_Until_death",Time.timeSinceLevelLoad }
        });

        Debug.Log(Result);
        Player.CanControlPlayer = false;
        UIManager.instance.DeathMenuEnable();
     
    }

    IEnumerator LoadYourAsyncScene(int index, bool TransferInv= false)
    {
        List<ListItem> inv = Inventory.instance.items;
        int[] CurrentScore = Player.gameObject.GetComponent<PlayerScore>().GetScore();
        GameObject weapon = Player.getWeapon();
        Equipment[] currentE = Player.gameObject.GetComponent<EquipmentManager>().currentEquipment;
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(Levels[index]);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        iniComponents();
        if (TransferInv)
        {
            TrasnferInvetory(inv, CurrentScore, weapon, currentE);
        }
        GameManager.CheckPoint = Vector3.zero;
        GameManager.CheckPointName = "StarCheckPoint";
        if(index == 2 && GameManager.progress[CurrentLevelIndex] == true)
        {
            HubClear();
        }

    }

    public void playSound(AudioClip sound)
    {
        audio.volume = 0.08f;
        audio.PlayOneShot(sound);
    }

    private void TrasnferInvetory(List<ListItem> inventory, int[] CurrentScore, GameObject wep, Equipment[] equip)
    {
        Player.gameObject.GetComponent<EquipmentManager>().currentEquipment = equip;
        Player.gameObject.GetComponent<EquipmentManager>().onItemEquipedCallBack();
        if (wep)
        {
            Player.SetWeapon(wep, true);
        }
        Inventory.instance.items = inventory;
        Inventory.instance.onItemChangedCallBack();
        PlayerScore ps =   Player.gameObject.GetComponent<PlayerScore>();
        ps.SetScore(CurrentScore);

    }

    private void HubClear()
    {

    }

  

}
