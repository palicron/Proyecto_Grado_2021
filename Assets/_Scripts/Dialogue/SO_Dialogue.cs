using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public enum DialogueType
{
    Dialogue,
    EndDialogue,
    Question,
    Trading,
    Asking,
    Information
}
[System.Serializable]
public class line
{
    public string DialogueDescription;
    public string LocutorName;
    public bool JumpQuestion;
    public int JumpTo;
    [TextArea(3, 10)]
    public string sentences;
    public DialogueType Type;
    [Header("Question")]
    [Space]
    public string[] Answers;
    public int CorrectAnswer;
    public int CorrectJump;
    public int InCorrectJump;
    [Header("Asking")]
    public int[] elementNeded;

    [Header("Information")]
    public string[] About;
    public int[] WhereToJumpInfo;


}


[CreateAssetMenu(fileName ="S_Dialogue",menuName="Dialogue/NpsDialogue")]
public class SO_Dialogue : ScriptableObject
{
   public bool bIsRandomDialogue;
   [Range(1,5)]
   public int DialoguePriority = 1;
   public line[] lines;



}





