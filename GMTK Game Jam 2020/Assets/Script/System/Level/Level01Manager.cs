using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Controla o roteiro do level.
/// </summary>
public class Level01Manager : MonoBehaviour
{
    public static Level01Manager instance;

    [SerializeField] private GameObject blackPanel;
    private DialogueTrigger currentTrigger = null;

    [Space]
    [Header("Primeira conversa")]
    private bool primeira_conversa_iniciada = false;

    [Space]
    [Header("Segunda conversa")]
    [HideInInspector] public int criaturas_derrotadas = 0;
    private bool segunda_conversa_iniciada = false;

    [Space]
    [Header("Terceira conversa")]
    [SerializeField] private Transform overlapPoint;
    [SerializeField] private Vector2 overlapSize;
    [SerializeField] private LayerMask oqEPlayer;
    public bool terceira_conversa_iniciada = false;

    [Space]
    [Header("Quarta conversa")]
    [SerializeField] private Vector2 direcao;
    [SerializeField] private float velocidade;
    public bool quarta_conversa_iniciada = false;
    private PlayerMovement player;


    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    private void Start()
    {
        //ao começar o jogo, abrir um painel preto com a caixa de dialogo inicial
        PrimeiraConversa();
        player = GameObject.Find("Player").GetComponent<PlayerMovement>();

        AudioManager.instance.FadeOutAudio("MenuMusic", 1f, this);
    }

    private void Update()
    {
        //fechar conversa
        if (currentTrigger.conversationEnded)
        {
            blackPanel.SetActive(false);
            currentTrigger.conversationEnded = false;
        }

        //segunda conversa ao derrotar as criaturas
        if(criaturas_derrotadas >= 2 && !segunda_conversa_iniciada)
        {
            SegundaConversa();
        }

        if (!terceira_conversa_iniciada)
        {
            Collider2D col = Physics2D.OverlapBox(overlapPoint.position, overlapSize, 0, oqEPlayer);
            if(col != null)
            {
                if (col.tag == "Player")
                    TerceiraConversa();
            }
        }
    }

    public void OnMusicFinished()
    {
        AudioManager.instance.FadeInAudio("MainMusic", 15f);
    }

    private void StartConversation(string triggerName, bool? canMove = null, bool? canInput = null)
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
            targetTrigger.TriggerDialogue(canMove, canInput);
            currentTrigger = targetTrigger;
        }

    }
  
    private void PrimeiraConversa()
    {
        blackPanel.SetActive(true);
        StartConversation("Primeira conversa", false, false);
    }

    private void SegundaConversa()
    {
        StartConversation("Segunda conversa", false, false);
        segunda_conversa_iniciada = true;
    }

    private void TerceiraConversa()
    {
        StartConversation("Terceira conversa", true, false);
        terceira_conversa_iniciada = true;
    }

    public void QuartaConversa()
    {
        quarta_conversa_iniciada = true;

        StartCoroutine(QuartaConversaEnumerator());
        player.velocidadeMoverAuto = 5f;
        player.direcaoMoverAuto = Vector2.right;
        player.moverAuto = true;
    }
    IEnumerator QuartaConversaEnumerator()
    {
        yield return new WaitForSeconds(2f);

        StartConversation("Quarta conversa");
    }

    public void NextLevelCol(int sceneIndex, Collider2D col, string colName)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(overlapPoint.position, overlapSize);
    }
}
