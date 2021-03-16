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

    private void Awake()
    {
        intance = this;
        // Cursor.lockState = CursorLockMode.Confined;
        DontDestroyOnLoad(this.gameObject);

    }
    void Start()
    {


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
        }
        else
        {
            UIManager.Instance.Pause();
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

    public void loadLevel(int index)
    {
        CurrentLevelIndex = index;
        GameManager.CheckPointProgres = 0;
        StartCoroutine(LoadYourAsyncScene(index));
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
        loadLevel(0);
    }

    IEnumerator LoadYourAsyncScene(int index)
    {

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(Levels[index]);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        iniComponents();
        GameManager.CheckPoint = Vector3.zero;
        GameManager.CheckPointName = "StarCheckPoint";

    }



  

}
