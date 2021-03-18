using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingPage : MonoBehaviour
{
    public Image icon;

    public Text name;

    public Text description;

    public Text paperRequirement;

    public Text plasticRequirement;

    public Text glassRequirement;

    public Text metalRequirement;

    public GameObject confirmText;

    PlayerScore playerScore;

    private int[] score;

    Recipe recipe;

    Color green = new Color32(0, 255, 0, 0);

    Color red = new Color32(255, 0, 0, 0);


    public void SetPageUI(Recipe pRecipe)
    {
        if(pRecipe == null)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
            recipe = pRecipe;
            icon.sprite = recipe.item.icon;
            name.text = recipe.item.name;
            description.text = recipe.item.description.ToString();
            paperRequirement.text = pRecipe.requirement[0].ToString();
            plasticRequirement.text = pRecipe.requirement[1].ToString();
            glassRequirement.text = pRecipe.requirement[2].ToString();
            metalRequirement.text = pRecipe.requirement[3].ToString();
        }
    }

    public void Craft()
    {
        Text pText = confirmText.GetComponent<Text>();
        int[] newScore = recipe.IsCraftable(playerScore.GetScore());
        playerScore.UpdateScore(3, 0);
        Debug.Log(newScore.Length); 
        if (newScore.Length==4)
        {
            playerScore.SetScore(newScore);
            pText.text = "Se recicló el item con éxito!";
            pText.color = green;
        }
        else
        {
            confirmText.SetActive(true);
            if(newScore.Length==0)
            {
                pText.text = "Materiales insuficientes...";
                pText.color = red;
            }
            else
            {
                pText.text = "El inventario está lleno...";
                pText.color = red;
            }
        }
        confirmText.GetComponent<Animator>().SetBool("itemRecycled", true);
    }

    public void SetPlayerScore(PlayerScore pScore)
    {
        playerScore = pScore;
    }
}
