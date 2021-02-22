using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{


    public static UIManager intance;
    public static UIManager Instance { get { if (intance == null) { intance = new UIManager(); } return intance; } }
    // Start is called before the first frame update

    
    [SerializeField]
    GameObject UiInvetory;
     GameObject UiHealth;
    private void Awake()
    {
        intance = this;
       // Cursor.lockState = CursorLockMode.Confined;
        DontDestroyOnLoad(this.gameObject);

        UiInvetory.SetActive(false);
      
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
