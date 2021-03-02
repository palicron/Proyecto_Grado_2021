using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager intance;
    public static DialogueManager Instance { get { if (intance == null) { intance = new DialogueManager(); } return intance; } }

    public static bool WaitingAswer = false;
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
            Debug.Log(ActiveLineIndex);
        }
        else
        {

            Currentsentece = CurrentConversation.lines[ActiveLineIndex].sentences;
            StopAllCoroutines();
            StartCoroutine(TypeSentence(Currentsentece));

        }
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

        if (CurrentConversation.lines[ActiveLineIndex].JumpQuestion)
        {
            ActiveLineIndex = CurrentConversation.lines[ActiveLineIndex].JumpTo;
        }
        else
        {
            ActiveLineIndex++;
        }
       
    }

    public void SendAswer(int Number)
    {
       
        if (Number == CorrectAswer)
        {
            ActiveLineIndex = CurrentConversation.lines[ActiveLineIndex-1].CorrectJump;
      
        }
        else
        {
            ActiveLineIndex = CurrentConversation.lines[ActiveLineIndex-1].InCorrectJump;
         
        }
        foreach (Button b in ASwerButton)
        {
            b.gameObject.SetActive(false);
        }
     
     
        DialogueManager.WaitingAswer = false;
        DisplayNextSentence();
    }
}
