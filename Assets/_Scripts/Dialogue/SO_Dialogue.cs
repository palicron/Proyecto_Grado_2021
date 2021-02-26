using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="S_Dialogue",menuName="Dialogue/NpsDialogue")]
public class SO_Dialogue : ScriptableObject
{
   public string LocutorName;
   public bool bIsRandomDialogue;
   
   [Range(1,5)]
   public int DialoguePriority = 1;
   [TextArea(3,10)]
   public string[] sentences;

}
