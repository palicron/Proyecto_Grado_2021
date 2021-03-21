using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ErrorDialog : MonoBehaviour
{
    #region Singleton

    public static ErrorDialog instance;

    void Awake()
    {
        instance = this;
    }
    #endregion

    public GameObject errorDialog;
    public Text text;

    public void ThrowError(string message)
    {
        errorDialog.GetComponent<Animator>().SetBool("showDialog", true);
        text.text = message;
    }
}
