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

    NPC CurrentNPCTalking = null;

    // Start is called before the first frame update

    private void Awake()
    {
        intance = this;
    }
    void Start()
    {
        senteces = new Queue<string>();
    }


    public void StarDialogue(SO_Dialogue Dialogue, NPC locutor)
    {

        senteces.Clear();
        DialoguePanel.SetActive(true);
        CurrentNPCTalking = locutor;
        CharaterName.text = Dialogue.LocutorName;

        if (Dialogue.bIsRandomDialogue)
        {
             int ran = Random.Range(0,Dialogue.sentences.Length);
             senteces.Enqueue(Dialogue.sentences[ran]);
        }
        else
        {
            foreach (string sentence in Dialogue.sentences)
            {
                senteces.Enqueue(sentence);
            }
        }


        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (senteces.Count == 0)
        {
            EndDialgue();
            return;
        }
        string sentece = senteces.Dequeue();

        DialogueText.text = sentece;

    }

    public void EndDialgue()
    {
        DialoguePanel.SetActive(false);
        CurrentNPCTalking.EndDialogue();
        CurrentNPCTalking = null;
        DialogueText.text = "";
        CharaterName.text = "";
    }
}
