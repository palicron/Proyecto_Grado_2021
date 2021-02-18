using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager intance;
    public static GameManager Instance { get { if (intance == null) { intance = new GameManager(); } return intance; } }
    // Start is called before the first frame update
    
    
    public GameObject[] salas;
    [SerializeField]
    public GameObject[,] Grid= new GameObject[2,2];

    private void Awake()
    {
        intance = this;
       // Cursor.lockState = CursorLockMode.Confined;
        DontDestroyOnLoad(this.gameObject);
      
    }
    void Start()
    {
       
        Grid[1,1] = salas[0];
          Grid[0,1] = salas[0];
            Grid[0,0] = salas[1];
              Grid[1,0] = salas[0];

           
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    static void ScreemTransition()
    {
        
    }
}
