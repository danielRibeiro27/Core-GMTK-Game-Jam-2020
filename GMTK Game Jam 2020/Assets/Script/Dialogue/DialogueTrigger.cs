using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue[] dialogue;
    public string dialogueName = "Primeiro diálogo";
    private int currentIndex = 0;
    public bool conversationEnded = false;
    private void Update()
    {
        AguardarFinish();

        if(currentIndex >= dialogue.Length)
        {
            conversationEnded = true;
        }
    }

    public void TriggerDialogue(int index = 0)
    {
        currentIndex = index;
        if(index < dialogue.Length)
        {
            DialogueManager.instance.StartDialogue(dialogue[index]);
        }
    }

    private void AguardarFinish()
    {
        if (DialogueManager.instance.currentDialogueFinished)
        {
            //ir para o proximo dialogo
            currentIndex++;
            TriggerDialogue(currentIndex);
        }
    }
}
