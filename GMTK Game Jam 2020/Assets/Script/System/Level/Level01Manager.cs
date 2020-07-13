using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controla o roteiro do level.
/// </summary>
public class Level01Manager : MonoBehaviour
{
    public static Level01Manager instance;

    [SerializeField] private GameObject blackPanel;
    [HideInInspector] public int criaturas_derrotadas = 0;
    private DialogueTrigger currentTrigger = null;
    private bool segunda_conversa_iniciada = false;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    private void Start()
    {
        //ao começar o jogo, abrir um painel preto com a caixa de dialogo inicial
        blackPanel.SetActive(true);
        StartConversation("Primeira conversa");
    }

    private void Update()
    {
        if (currentTrigger.conversationEnded)
        {
            blackPanel.SetActive(false);
            currentTrigger.conversationEnded = false;
        }

        if(criaturas_derrotadas >= 2 && !segunda_conversa_iniciada)
        {
            StartConversation("Segunda conversa");
            segunda_conversa_iniciada = true;
        }
    }

    private void StartConversation(string triggerName)
    {
        //busca o trigger corespondente
        DialogueTrigger[] triggers = FindObjectsOfType<DialogueTrigger>();
        DialogueTrigger targetTrigger = null;
        foreach(DialogueTrigger tri in triggers)
        {
            if(tri.dialogueName == triggerName)
            {
                targetTrigger = tri;
                break;
            }
        }

        //ativa os dialogos um após o outro
        if(targetTrigger != null)
        {
            targetTrigger.TriggerDialogue();
            currentTrigger = targetTrigger;
        }

    }
  
}
