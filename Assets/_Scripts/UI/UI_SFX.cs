using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_SFX : MonoBehaviour
{
    #region Singleton
    public static UI_SFX instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one UI_SFX instance found");
        }
        instance = this;
    }
    #endregion

    AudioSource audio;

    public AudioClip turnPage;
    public AudioClip openEquipment;
    public AudioClip closeEquipment;
    public AudioClip openInventory;
    public AudioClip closeInventory;
    public AudioClip clickButton;
    public AudioClip rightAnswer;
    public AudioClip wrongAnswer;
    public AudioClip pickUp;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    public void PlayTurnPage()
    {
        audio.volume = 0.3f;
        audio.PlayOneShot(turnPage);
    }

    public void PlayInventory(bool p)
    {
        Debug.Log("Sonando inventario");
        audio.volume = 0.15f;
        if (p)
        {
            audio.PlayOneShot(openInventory);
        }
        else
        {
            audio.PlayOneShot(closeInventory);
        }
    }
    public void PlayEquipment(bool p)
    {
        Debug.Log("Sonando equipamiento");
        audio.volume = 0.08f;
        if (p)
        {
            audio.PlayOneShot(openEquipment);
        }
        else
        {
            audio.PlayOneShot(closeEquipment);
        }
    }

    public void PlayClickButton()
    {
        audio.volume = 0.05f;
        audio.PlayOneShot(turnPage);
    }

    public void PlayAnswer(bool p)
    {
        if(p)
        {
            audio.volume = 0.05f;
            audio.PlayOneShot(rightAnswer);
        }
        else
        {
            audio.volume = 0.05f;
            audio.PlayOneShot(wrongAnswer);
        }
    }

    public void PlayPickUp()
    {
        audio.volume = 0.05f;
        audio.PlayOneShot(pickUp);
    }
}
