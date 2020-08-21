using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroManager : MonoBehaviour
{
    private DialogueTrigger currentTrigger = null;

    void Start()
    {
        StartConversation("Intro conversation", false, false);
    }

    private void StartConversation(string triggerName, bool? canMove = null, bool? canInput = null)
    {
        //busca o trigger corespondente
        DialogueTrigger[] triggers = FindObjectsOfType<DialogueTrigger>();
        DialogueTrigger targetTrigger = null;
        foreach (DialogueTrigger tri in triggers)
        {
            if (tri.dialogueName == triggerName)
            {
                targetTrigger = tri;
                break;
            }
        }

        //ativa os dialogos um após o outro
        if (targetTrigger != null)
        {
            targetTrigger.TriggerDialogue(canMove, canInput);
            currentTrigger = targetTrigger;
        }

    }
}
