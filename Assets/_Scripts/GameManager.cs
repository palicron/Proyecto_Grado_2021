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

    [SerializeField]
    string[] Levels;
    public GameObject[] salas;
    public PlayerCtr Player;

    [SerializeField]
    public EquipmentManager Equipment;
    [SerializeField]

    float YkillZone;
  

    public bool StarLoad = false;

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
            Player.transform.position = GameManager.CheckPoint;
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
            hs.Init();
            UIManager.Instance.UpdatePlayerLife(hs.getHealthPorcentage());
        }
       

    }

    public void loadLevel(int index)
    {
        StartCoroutine(LoadYourAsyncScene(index));
    }
    public void ExitGame()
    {
        Application.Quit();
    }

    IEnumerator LoadYourAsyncScene(int index)
    {

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(Levels[index]);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        iniComponents();

    }



  

}
