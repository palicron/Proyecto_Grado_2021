using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager intance;
    public static DialogueManager Instance { get { if (intance == null) { intance = new DialogueManager(); } return intance; } }
    Queue<string> senteces;
    [SerializeField]
    public GameObject DialoguePanel;
    [SerializeField]
    Text CharaterName;
    [SerializeField]
    Text DialogueText;

    [SerializeField, Range(0, 1)]
    float textSpeed;
    bool IsinConversation;
    NPC CurrentNPCTalking = null;

    bool IsTyping = false;

    private int ActiveLineIndex = 0;

    SO_Dialogue CurrentConversation = null;

    bool ToEndConversation =false;

    string Currentsentece = "";
    // Start is called before the first frame update

    private void Awake()
    {
        intance = this;
    }

    void Update()
    {
        if(IsinConversation)
        {

        }
    }
    void Start()
    {
       // senteces = new Queue<string>();
    }


    public void StarDialogue(SO_Dialogue Dialogue,NPC locutor)
    {

     //   senteces.Clear();
        DialoguePanel.SetActive(true);
        ActiveLineIndex = 0;
        CurrentConversation = Dialogue;
        CharaterName.text = CurrentConversation.lines[ActiveLineIndex].LocutorName;
        CurrentNPCTalking = locutor;
        
        if (Dialogue.bIsRandomDialogue)
        {
            DisplayRandomLine();
         }
         else
        {
            DisplayNextSentence();
        }
    
    }

    public void DisplayNextSentence()
    {

        if(ActiveLineIndex< CurrentConversation.lines.Length && !ToEndConversation)
        {
            DisplayLine();

            
            if(CurrentConversation.lines[ActiveLineIndex].IsAendLine)
            {
                ToEndConversation = true;
            }

        }
        else
        {
            EndDialgue();
        }
    }
    void DisplayRandomLine()
    {
        ActiveLineIndex = Random.Range(0, CurrentConversation.lines.Length);
        DisplayNextSentence();
        ToEndConversation = true;

    }
    void DisplayLine()
    {
        if (IsTyping)
        {
            StopAllCoroutines();
            DialogueText.text = CurrentConversation.lines[ActiveLineIndex].sentences;
            IsTyping = false;
            ActiveLineIndex++;
        }
        else
        {
          
            Currentsentece = CurrentConversation.lines[ActiveLineIndex].sentences;
            StopAllCoroutines();
            StartCoroutine(TypeSentence(Currentsentece));

        }
    }

    public void EndDialgue()
    {
        DialoguePanel.SetActive(false);
        CurrentNPCTalking.EndDialogue();
        CurrentNPCTalking = null;
        DialogueText.text = "";
        CharaterName.text = "";
        Currentsentece = "";
        ActiveLineIndex = 0;
        CurrentConversation = null;
        ToEndConversation = false;

    }
    IEnumerator TypeSentence(string sentece)
    {
        IsTyping = true;
        DialogueText.text = "";
        foreach (char letter in sentece.ToCharArray())
        {
            DialogueText.text += letter;
            yield return new WaitForSeconds(textSpeed);
        }
     
        IsTyping = false;
        ActiveLineIndex++;
    }
}
