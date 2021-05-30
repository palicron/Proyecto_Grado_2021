using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BootsTexture : MonoBehaviour
{
    public Material bDefault;
    public Material bCommon;
    public Material bRare;
    public Material bUltraRare;
    public Material bLegendary;
    public Material bUnique;

    Material[] materialList;

    public GameObject skin;

    void Awake()
    {
        Material[] aux = { bDefault, bCommon, bRare, bUltraRare, bLegendary, bUnique };
        materialList = aux;
    }

    public void SetTexture(int i)
    {
        skin.GetComponent<Renderer>().material = materialList[i];
    }
}
