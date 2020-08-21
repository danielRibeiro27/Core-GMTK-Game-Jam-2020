using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level03Manager : MonoBehaviour
{
    public static Level03Manager instance;

    [Space]
    [Header("Primeira conversa")]
    [SerializeField] private GameObject blackPanel;
    private bool primeira_conversa_iniciada = false;


    [Space]
    [Header("Segunda conversa")]
    private bool segunda_conversa_iniciada = false;
    public Transform segunda_conversa_position;
    public Vector2 segunda_conversa_col_size;
    public LayerMask oqEPlayer;

    [Space]
    [Header("Terceira conversa iniciada")]
    private bool terceira_conversa_iniciada = false;
    public Transform terceira_conversa_position;
    public Vector2 terceira_convresa_col_size;

    [Space]
    [Header("Outros")]
    private DialogueTrigger currentTrigger = null;

    void Start()
    {
        if(!primeira_conversa_iniciada)
            PrimeiraConversaLv3();
    }

    void Update()
    {
        //fechar conversa
        if (currentTrigger.conversationEnded)
        {
            blackPanel.SetActive(false);
            currentTrigger.conversationEnded = false;
        }

        Collider2D col = Physics2D.OverlapBox(segunda_conversa_position.position, segunda_conversa_col_size, 0, oqEPlayer);
        if (col != null)
        {
            if (!segunda_conversa_iniciada)
            {
                SegundaConversaLv3();
            }
        }

        Collider2D colt = Physics2D.OverlapBox(terceira_conversa_position.position, terceira_convresa_col_size, 0, oqEPlayer);
        if (colt != null)
        {
            if (!terceira_conversa_iniciada)
            {
                TerceiraConversaLv3();
            }
        }
    }

    private void PrimeiraConversaLv3()
    {
        primeira_conversa_iniciada = true;

        blackPanel.SetActive(true);
        StartConversation("Primeira conversa Lv3", false, false);
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

    public void SegundaConversaLv3()
    {
        segunda_conversa_iniciada = true;
        StartConversation("Segunda conversa Lv3", false, false);
    }

    public void TerceiraConversaLv3()
    {
        terceira_conversa_iniciada = true;
        StartConversation("Terceira conversa Lv3", false, false);
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(segunda_conversa_position.position, segunda_conversa_col_size);

        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(terceira_conversa_position.position, terceira_convresa_col_size);
    }
}
