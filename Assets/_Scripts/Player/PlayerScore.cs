using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScore : MonoBehaviour
{
    private int[] score = new int[4];

    GameObject scoreUI;

    public Text[] scoreUIText = new Text[4];

    void Awake()
    {
        scoreUI = GameObject.Find("PF_GameUI/Score/Recipient");
        scoreUIText[0] = scoreUI.transform.Find("paper").transform.Find("Text").GetComponent<Text>();
        scoreUIText[1] = scoreUI.transform.Find("plastic").transform.Find("Text").GetComponent<Text>();
        scoreUIText[2] = scoreUI.transform.Find("glass").transform.Find("Text").GetComponent<Text>();
        scoreUIText[3] = scoreUI.transform.Find("metal").transform.Find("Text").GetComponent<Text>();
    }

    public void UpdateScore(int element, int augment)
    {
        score[element] += augment;
        scoreUIText[element].text = score[element].ToString();
    }
}
