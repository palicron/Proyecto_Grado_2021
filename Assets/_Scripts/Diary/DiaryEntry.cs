using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Diary/Entry")]
public class DiaryEntry : ScriptableObject
{
    public int id;
    public string title;
    public string sourceName;
    public string description;


}
