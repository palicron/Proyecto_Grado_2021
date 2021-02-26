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

    NPC CurrentNPCTalking = null;

    bool IsTyping = false;

    string Currentsentece = "";
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
            int ran = Random.Range(0, Dialogue.sentences.Length);
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
        if (IsTyping)
        {
            StopAllCoroutines();
            DialogueText.text = Currentsentece;
            IsTyping = false;
        }
        else
        {
            if (senteces.Count == 0)
            {
                EndDialgue();
                return;
            }
            Currentsentece = senteces.Dequeue();
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
    }
}
