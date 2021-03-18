using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuInfo : MonoBehaviour
{

    public GameObject recipes;

    public GameObject materials;

    public GameObject index;

    public void Recipes()
    {
        recipes.SetActive(true);
        index.SetActive(false);
    }

    public void Materials()
    {
        materials.SetActive(true);
        index.SetActive(false);
    }

    public void BackToIndex()
    {
        recipes.SetActive(false);
        materials.SetActive(false);
        index.SetActive(true);
    }

    // Start is called before the first frame update
    public void Close()
    {
        gameObject.SetActive(false);
    }

}
