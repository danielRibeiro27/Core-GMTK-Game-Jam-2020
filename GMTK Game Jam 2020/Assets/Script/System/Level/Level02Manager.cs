using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level02Manager : MonoBehaviour
{
    public static Level02Manager instance;

    [Space]
    [Header("Primeira conversa")]
    [SerializeField] private GameObject blackPanel;
    private bool primeira_conversa_iniciada = false;


    [Space]
    [Header("Segunda conversa")]
    [SerializeField] private Transform card;
    [SerializeField] private Transform card_block_colision;
    [SerializeField] private Vector2 cardColSize;
    [SerializeField] private LayerMask oqEPlayer;
    private bool segunda_conversa_iniciada = false;


    [Space]
    [Header("Outros")]
    [SerializeField] private PlayerMovement player;
    public bool hasKey = false;
    private DialogueTrigger currentTrigger = null;

    void Start()
    {
        PrimeiraConversaLv2();
    }

    void Update()
    {
        //fechar conversa
        if (currentTrigger.conversationEnded)
        {
            blackPanel.SetActive(false);
            currentTrigger.conversationEnded = false;
        }

        if(card != null)
        {
            Collider2D col = Physics2D.OverlapBox(card.position, cardColSize, 0, oqEPlayer);
            if(col != null)
            {
                hasKey = true;
                Destroy(card.gameObject);
                Destroy(card_block_colision.gameObject);
            }
        }
    }

    private void PrimeiraConversaLv2()
    {
        primeira_conversa_iniciada = true;

        blackPanel.SetActive(true);
        StartConversation("Primeira conversa Lv2", false, false);
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

    public void SegundaConversaLv2()
    {
        segunda_conversa_iniciada = true;
        StartConversation("Segunda conversa Lv2", true, false);
    }

    public void MoverPlayerAuto()
    {
        player.velocidadeMoverAuto = 5f;
        player.direcaoMoverAuto = Vector2.right;
        player.moverAuto = true;
    }

    public void NextLevelCol(int sceneIndex, Collider2D col, string colName)
    {
        if (hasKey && colName == "Segundo diálogo collision")
        {
            SegundaConversaLv2();
        }

        if (hasKey && colName == "Passar fase após segundo diálogo")
        {
            SceneManager.LoadScene(sceneIndex);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(card.transform.position, cardColSize);
    }
}
