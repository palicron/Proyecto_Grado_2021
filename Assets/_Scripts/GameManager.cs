using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager intance;
    public static GameManager Instance { get { if (intance == null) { intance = new GameManager(); } return intance; } }
    // Start is called before the first frame update

    public static Vector3 CheckPoint = Vector3.zero;
    public GameObject[] salas;
    [SerializeField]
    public GameObject[,] Grid= new GameObject[2,2];
    
    public PlayerCtr Player;
    [SerializeField]

    public EquipmentManager Equipment;
    [SerializeField]

    float YkillZone;

    UIManager Uimanager;
    
    private void Awake()
    {
        intance = this;
       // Cursor.lockState = CursorLockMode.Confined;
        DontDestroyOnLoad(this.gameObject);
      
    }
    void Start()
    {
     
      Uimanager = UIManager.Instance;
      //recordad que esto solo e spara le nivel de prueba tiene que estar es cuando se carga un lvl
      iniComponents();
        
    }

    // Update is called once per frame
    void Update()
    {
       
        if(Player.transform.position.y <= YkillZone)
        {
            Player.transform.position = GameManager.CheckPoint;
        }
    }


   



    public void PauseGame()
    {
        if(UIManager.GameIsPaused)
        {
            Uimanager.Resume();
        }
        else
        {
            Uimanager.Pause();
        }
    }

    void iniComponents()
    {
        
        if(Player)
        {
            healthsystems hs =  Player.GetComponent<healthsystems>();
            hs.healthUpdate += Uimanager.UpdatePlayerLife;
            hs.Init();
            Uimanager.UpdatePlayerLife( hs.getHealthPorcentage());
            
        }
        else
        {
             Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCtr>();
            Player.GetComponent<healthsystems>().healthUpdate += Uimanager.UpdatePlayerLife;
            healthsystems hs = Player.GetComponent<healthsystems>();
            hs.healthUpdate += Uimanager.UpdatePlayerLife;
            hs.Init();
            Uimanager.UpdatePlayerLife(hs.getHealthPorcentage());
        }
         
          
    }


}
