using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
[System.Serializable]
public class line
{
    public string LocutorName;
    [TextArea(3, 10)]
    public string sentences;
    public bool IsAendLine;
    public bool IsAquestion;
    public int JumpLine;
    public bool Istrade;
    public bool IsAskingFormats;

}


[CreateAssetMenu(fileName ="S_Dialogue",menuName="Dialogue/NpsDialogue")]

public class SO_Dialogue : ScriptableObject
{
   public bool bIsRandomDialogue;

   [Range(1,5)]
   public int DialoguePriority = 1;

    public line[] lines;



}



