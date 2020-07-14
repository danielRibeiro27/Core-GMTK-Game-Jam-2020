using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue[] dialogue;
    public string dialogueName;
    private int currentIndex = 0;
    public bool conversationEnded = false;
    private bool foi_utilizado = false;
    private void Update()
    {
        AguardarFinish();

        if(currentIndex >= dialogue.Length)
        {
            if (foi_utilizado)
            {
                conversationEnded = true;
                GameManager.CanInput = true;

                if (dialogueName == "Terceira conversa")
                {
                    Level01Manager lvl = FindObjectOfType<Level01Manager>();
                    if (lvl.terceira_conversa_iniciada && !lvl.quarta_conversa_iniciada)
                    {
                        lvl.QuartaConversa();
                    }
                }

                Destroy(gameObject);
            }
        }
    }

    public void TriggerDialogue(int index = 0)
    {
        GameManager.CanInput = false;
        currentIndex = index;
        foi_utilizado = true;
        if(index < dialogue.Length)
        {
            DialogueManager.instance.StartDialogue(dialogue[index]);
            DialogueManager.instance.currentDialogueName = dialogueName;
        }
    }

    private void AguardarFinish()
    {
        if (DialogueManager.instance.currentDialogueFinished)
        {
            if(DialogueManager.instance.currentDialogueName == dialogueName)
            {
                //ir para o proximo dialogo
                currentIndex++;
                TriggerDialogue(currentIndex);
            }
        }
    }
}
