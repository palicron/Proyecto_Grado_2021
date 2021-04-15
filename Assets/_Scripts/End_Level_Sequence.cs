using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End_Level_Sequence : NPC
{
    [SerializeField]
    int level = 3;
    [SerializeField]
    PlayerCtr player;
    [SerializeField]
    GameObject playerCam;
    [SerializeField]
    GameObject SecondCam;
    [SerializeField]
    float TimeToendLevel;
    float timeTOend = 0;
    public override void EndDialogue()
    {
        StartCoroutine(endLevel());
    }

    public override void midDialgueAction()
    {
        throw new System.NotImplementedException();
    }

    protected override void Interect()
    {
        throw new System.NotImplementedException();
    }

    protected override void TriggerDialogue()
    {
        bIsInConversation = true;
        player.cancelAllMovment();
        player.CanControlPlayer = false;
        DialogueManager.intance.AutomaticDialogue(Dialogues[0], this);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            TriggerDialogue();

        }
    }

    IEnumerator endLevel()
    {

        SecondCam.SetActive(true);
        playerCam.SetActive(false);
        GameManager.progress[2] = true;
        while (timeTOend< TimeToendLevel)
        {
            timeTOend += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }


        GameManager.intance.loadLevel(2, true);
    }
}
