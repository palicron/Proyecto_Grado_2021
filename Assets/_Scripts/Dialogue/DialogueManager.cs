using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager intance;
    public static DialogueManager Instance { get { if (intance == null) { intance = new DialogueManager(); } return intance; } }

    public static bool WaitingAswer = false;

    public static bool IsinConversation =false;
 
    public GameObject DialoguePanel;
    public GameObject RemoteDialoguePanel;
    [SerializeField]
    Text CharaterName;
    [SerializeField]
    Text DialogueText;
    [SerializeField]
    Text RemoteCharaterName;
    [SerializeField]
    Text RemoteDialogueText;

    [SerializeField, Range(0, 1)]
    float textSpeed;
    [SerializeField, Range(0, 1)]
    float textAutoWait = 0.5f;
    //public bool IsinConversation;
    NPC CurrentNPCTalking = null;

    [SerializeField]
    Button[] ASwerButton;

    bool IsTyping = false;

    private int ActiveLineIndex = 0;

    SO_Dialogue CurrentConversation = null;

    bool ToEndConversation = false;


    string Currentsentece = "";

    int CorrectAswer = -1;
    // Start is called before the first frame update

    private void Awake()
    {
        intance = this;
    }

    void Update()
    {


        if (IsinConversation)
        {

        }
    }
    void Start()
    {
        // senteces = new Queue<string>();
    }


    public void StarDialogue(SO_Dialogue Dialogue, NPC locutor)
    {

        //   senteces.Clear();
        DialoguePanel.SetActive(true);
        ActiveLineIndex = 0;
        CurrentConversation = Dialogue;
        DialogueManager.IsinConversation = true;
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

        if (ActiveLineIndex < CurrentConversation.lines.Length && !ToEndConversation)
        {


            switch (CurrentConversation.lines[ActiveLineIndex].Type)
            {
                case DialogueType.EndDialogue:
                    ToEndConversation = true;
                    break;
                case DialogueType.Question:
                    DisplayQuestion();
                    break;
                case DialogueType.Information:
                    DisplayInforQuestion();
                    break;
                case DialogueType.Trading:
                    break;
                case DialogueType.Asking:
                    break;
                default:
                    break;
            }



            DisplayLine();

        }
        else if (ToEndConversation && IsTyping)
        {
            DisplayLine();
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
            if(CurrentConversation.lines[ActiveLineIndex].JumpQuestion)
            {
                ActiveLineIndex = CurrentConversation.lines[ActiveLineIndex].JumpTo;
            }
            else
            {
                ActiveLineIndex++;
            }
   
        }
        else
        {

            Currentsentece = CurrentConversation.lines[ActiveLineIndex].sentences;
            StopAllCoroutines();
            StartCoroutine(TypeSentence(Currentsentece));

        }
    }

    void DisplayAutoLine()
    {
             Currentsentece = CurrentConversation.lines[ActiveLineIndex].sentences;
            StopAllCoroutines();
            StartCoroutine(TypeSentence(Currentsentece));
    }
    void DisplayQuestion()
    {
        CorrectAswer = CurrentConversation.lines[ActiveLineIndex].CorrectAnswer;
        DialogueManager.WaitingAswer = true;

        for (int i = 0; i < CurrentConversation.lines[ActiveLineIndex].Answers.Length; i++)
        {
            ASwerButton[i].gameObject.SetActive(true);
            ASwerButton[i].GetComponentInChildren<Text>().text = CurrentConversation.lines[ActiveLineIndex].Answers[i];
        }
    }

    void DisplayInforQuestion()
    {
        DialogueManager.WaitingAswer = true;

        for (int i = 0; i < CurrentConversation.lines[ActiveLineIndex].About.Length; i++)
        {
            ASwerButton[i].gameObject.SetActive(true);
            ASwerButton[i].GetComponentInChildren<Text>().text = CurrentConversation.lines[ActiveLineIndex].About[i];
        }
    }
    public void EndDialgue()
    {
        DialoguePanel.SetActive(false);
        CurrentNPCTalking.EndDialogue();
        DialogueManager.IsinConversation = false;
        CurrentNPCTalking = null;
        DialogueText.text = "";
        CharaterName.text = "";
        Currentsentece = "";
        ActiveLineIndex = 0;
        CurrentConversation = null;
        ToEndConversation = false;
        DialogueManager.WaitingAswer = false;
        foreach (Button b in ASwerButton)
        {
            b.gameObject.SetActive(false);
        }

    }
    public void EndAutoDialgue()
    {
        RemoteDialoguePanel.SetActive(false);
        DialogueManager.IsinConversation = false;
        CurrentConversation = null;
        ToEndConversation = false;
        DialogueManager.WaitingAswer = false;
        Currentsentece = "";
        RemoteCharaterName.text = "";
        RemoteCharaterName.text = "";
        ActiveLineIndex = 0;
        if (CurrentNPCTalking)
        {
            CurrentNPCTalking.EndDialogue();
        }
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

        if (CurrentConversation.lines[ActiveLineIndex].JumpQuestion)
        {
            ActiveLineIndex = CurrentConversation.lines[ActiveLineIndex].JumpTo;
        }
        else
        {
            ActiveLineIndex++;
        }
       
    }

    IEnumerator AutoTypeSentence(string sentece)
    {
        IsTyping = true;
        RemoteDialogueText.text = "";
        foreach (char letter in sentece.ToCharArray())
        {
            RemoteDialogueText.text += letter;
            yield return new WaitForSeconds(textSpeed);
        }

        IsTyping = false;
        ActiveLineIndex++;
    }

    public void SendAswer(int Number)
    {
       if(CurrentConversation.lines[ActiveLineIndex-1].Type == DialogueType.Question)
        {
            if (Number == CorrectAswer)
            {
                ActiveLineIndex = CurrentConversation.lines[ActiveLineIndex - 1].CorrectJump;

            }
            else
            {
                ActiveLineIndex = CurrentConversation.lines[ActiveLineIndex - 1].InCorrectJump;

            }
        }
       else
        {
            ActiveLineIndex = CurrentConversation.lines[ActiveLineIndex - 1].WhereToJumpInfo[Number];
        }
        foreach (Button b in ASwerButton)
        {
            b.gameObject.SetActive(false);
        }

        DialogueManager.WaitingAswer = false;
        DisplayNextSentence();
    }

    public void AutomaticDialogue(SO_Dialogue Dialogue, NPC locutor)
    {
        RemoteDialoguePanel.SetActive(true);
        ActiveLineIndex = 0;
        CurrentConversation = Dialogue;
        DialogueManager.IsinConversation = true;
        CharaterName.text = CurrentConversation.lines[ActiveLineIndex].LocutorName;
        CurrentNPCTalking = locutor;

        StartCoroutine(AutoDialogueStar());
    }


    IEnumerator  AutoDialogueStar()
    {
        int leng = CurrentConversation.lines.Length;
        Currentsentece = CurrentConversation.lines[ActiveLineIndex].sentences;
       
        while (CurrentConversation.lines[ActiveLineIndex].Type!= DialogueType.EndDialogue && ActiveLineIndex <= leng)
        {
            RemoteCharaterName.text = CurrentConversation.lines[ActiveLineIndex].LocutorName;
            yield return StartCoroutine(AutoTypeSentence(Currentsentece));
            yield return new WaitForSeconds(textAutoWait);
  
            Currentsentece = CurrentConversation.lines[ActiveLineIndex].sentences;
        }
        EndAutoDialgue();
    }
}
