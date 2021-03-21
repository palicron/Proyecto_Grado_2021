using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScore : MonoBehaviour
{
    private int[] score;

    GameObject scoreUI;

    Text[] scoreUIText = new Text[4];

    void Start()
    {
        //PlayerPrefsX.SetIntArray("Score", new int[4]);
        score = PlayerPrefsX.GetIntArray("Score", 0, 4);
        scoreUI = GameObject.Find("PF_GameUI/Score/Recipient");
        scoreUIText[0] = scoreUI.transform.Find("paper").transform.Find("Quantity").GetComponent<Text>();
        scoreUIText[1] = scoreUI.transform.Find("plastic").transform.Find("Quantity").GetComponent<Text>();
        scoreUIText[2] = scoreUI.transform.Find("glass").transform.Find("Quantity").GetComponent<Text>();
        scoreUIText[3] = scoreUI.transform.Find("metal").transform.Find("Quantity").GetComponent<Text>();
        RefreshInterface();
    }

    public void UpdateScore(int element, int augment)
    {
        score[element] += augment;
        scoreUIText[element].text = score[element].ToString();
        PlayerPrefsX.SetIntArray("Score", score);
    }

    public void SetScore(int[] newScore)
    {
        score = newScore;
        PlayerPrefsX.SetIntArray("Score", score);
        RefreshInterface();
    }

    public void RefreshInterface()
    {
        for (int i = 0; i < score.Length; i++)
        {
            UpdateScore(i, 0);
        }
    }
    public int[] GetScore()
    {
        return score;
    }
}
