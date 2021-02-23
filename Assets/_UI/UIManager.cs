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
    GameObject UiInvetory;

    [SerializeField]
    Slider UiHealth;

    [SerializeField]
    GameObject PauseMenu;
    [SerializeField]
    float LifeBarSpeedDecrese =0.2f;
    private void Awake()
    {
        instance = this;
        // Cursor.lockState = CursorLockMode.Confined;


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

    public void UpdatePlayerLife(float life)
    {
       StopAllCoroutines();
       StartCoroutine(DecreseLifeBar(life));
        
    }

    IEnumerator DecreseLifeBar(float newlife)
    {
        float currentValue = UiHealth.value;
        while (UiHealth.value > newlife)
        {
            UiHealth.value -= LifeBarSpeedDecrese * Time.deltaTime;
            yield return new WaitForEndOfFrame();

        }
        UiHealth.value = Mathf.Clamp( UiHealth.value,newlife,1);

    }

}
