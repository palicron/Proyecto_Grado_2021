using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{


    public static UIManager instance;
    public static UIManager Instance { get { if (instance == null) { instance = new UIManager(); } return instance; } }
    // Start is called before the first frame update



    public static bool GameIsPaused = false;

    [SerializeField]
    Slider UiHealth;

    [SerializeField]
    GameObject PauseMenu;
    [SerializeField]
    float LifeBarSpeedDecrese = 0.2f;

    [SerializeField]
    GameObject DeathMenu;
    [SerializeField]
    GameObject FinishMenu;
    private void Awake()
    {
        instance = this;
       // Cursor.lockState = CursorLockMode.Confined;

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
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Pause()
    {
        GameIsPaused = true;
        PauseMenu.SetActive(true);
        Time.timeScale = 0.0f;
        Cursor.lockState = CursorLockMode.Confined;

    }

    public void UpdatePlayerLife(float life)
    {
        StopAllCoroutines();
        if(life>=0)
        {
            StartCoroutine(DecreseLifeBar(life));
        }
        else
        {
            StartCoroutine(IncreseLife( Mathf.Abs( life)));
        }
        

    }

    IEnumerator DecreseLifeBar(float newlife)
    {
        float currentValue = UiHealth.value;
        while (UiHealth.value > newlife)
        {
            UiHealth.value -= LifeBarSpeedDecrese * Time.deltaTime;
            yield return new WaitForEndOfFrame();

        }
        UiHealth.value = Mathf.Clamp(UiHealth.value, newlife, 1);

    }

    IEnumerator IncreseLife(float newlife)
    {
        float currentValue = UiHealth.value;
        while (UiHealth.value < newlife)
        {
            UiHealth.value += LifeBarSpeedDecrese * Time.deltaTime;
            yield return new WaitForEndOfFrame();

        }
        UiHealth.value = Mathf.Clamp(UiHealth.value, newlife, 1);

    }


    public void LoadLevel(int index)
    {
        Resume();
        GameManager.intance.loadLevel(index);
    }

    public void DeathMenuEnable()
    {
        Pause();
        DeathMenu.SetActive(true);
    }
    public void endlevel()
    {
        FinishMenu.SetActive(true);
    }

    public void RestarFromCheckPoint()
    {
        Resume();
        GameManager.intance.RestarLevelCheckPoint();
    }

}
