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
                GameManager.CanMove = true;

                if (dialogueName == "Terceira conversa")
                {
                    GameManager.CanInput = false;
                    GameManager.CanMove = true;

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

    public void TriggerDialogue(bool? canMove, bool? canInput, int index = 0)
    {
        GameManager.CanInput = canInput != null ? (bool)canInput : GameManager.CanInput; //se especificar um canInput, usa ele, senao, mantem o valor
        GameManager.CanMove = canMove != null ? (bool)canMove : GameManager.CanMove; //se especificar um canMove, usa ele, senao, mantem o valor
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
                TriggerDialogue(null, null, currentIndex);
            }
        }
    }
}
