using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{


    public static UIManager instance;
    public static UIManager Instance { get { if (instance == null) { instance = new UIManager(); } return instance; } }
    // Start is called before the first frame update
  
    

      public static bool GameIsPaused = false;
    [SerializeField]
     GameObject UiInvetory;

     [SerializeField]
     GameObject UiHealth;

     [SerializeField]
     GameObject PauseMenu;
    private void Awake()
    {
        instance = this;
       // Cursor.lockState = CursorLockMode.Confined;
        DontDestroyOnLoad(this.gameObject);

        UiInvetory.SetActive(false);
      
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    public void Resume()
    {
         GameIsPaused = false;
         PauseMenu.SetActive(false);
         Time.timeScale = 1.0f;
    }

    public void Pause()
    {
         GameIsPaused = true;
         PauseMenu.SetActive(true);
         Time.timeScale = 0.0f;

    }
}
