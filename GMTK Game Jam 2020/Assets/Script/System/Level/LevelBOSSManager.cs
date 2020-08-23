using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelBOSSManager : MonoBehaviour
{
    public static LevelBOSSManager instance;

    [Space]
    [Header("Primeira conversa")]
    private bool primeira_conversa_iniciada = false;


    [Space]
    [Header("Segunda conversa")]
    private bool segunda_conversa_iniciada = false;

    [Space]
    [Header("Terceira conversa iniciada")]
    private bool terceira_conversa_iniciada = false;


    [Space]
    [Header("Outros")]
    [SerializeField] private GameObject atlasMetade;
    [SerializeField] private Transform atlasMetadedPosition;
    [SerializeField] private Image blackPanel;
    private DialogueTrigger currentTrigger = null;
    private GameObject player;
    private bool control = false;
    void Start()
    {
        if (!primeira_conversa_iniciada)
            PrimeiraConversaLvBOSS();

        player = GameObject.Find("Player");

        AudioManager.instance.StopAllAudios();
    }

    void Update()
    {
        //fechar conversa
        if (currentTrigger.conversationEnded)
        {
            currentTrigger.conversationEnded = false;
        }

        if(player != null)
        {
            if (player.GetComponent<PlayerCombat>().Vida <= 0 && !control)
            {
                TerceiraConversaLvBOSS();
            }
        }
    }

    private void PrimeiraConversaLvBOSS()
    {
        primeira_conversa_iniciada = true;
        FindObjectOfType<BossIA>().canMove = false;

        StartConversation("Primeira conversa LvBOSS", false, false);
    }

    public void SegundaConversaLvBOSS()
    {
        segunda_conversa_iniciada = true;
        FindObjectOfType<BossIA>().canMove = false;
        StartConversation("Segunda conversa LvBOSS", false, false);
    }

    private void TerceiraConversaLvBOSS()
    {
        control = true;
        FindObjectOfType<BossIA>().canMove = false;

        StartConversation("Terceira conversa LvBOSS");
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

    private void OnDrawGizmosSelected()
    {
    }

    public IEnumerator SpriteFade(float endValue, float duration)
    {
        float elapsedTime = 0;
        float startValue = blackPanel.color.a;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startValue, endValue, elapsedTime / duration);
            blackPanel.color = new Color(blackPanel.color.r, blackPanel.color.g, blackPanel.color.b, newAlpha);
            yield return null;
        }

        AudioManager.instance.PlayByName("ExplosaoLevelBOSS");

        yield return new WaitForSeconds(2f);

        LevelManager lv = FindObjectOfType<LevelManager>();
        lv.GoToLevel(6);
    }

    public void EventoOlharParaTela()
    {
        Animator anim = player.GetComponentInChildren<Animator>();
        anim.SetTrigger("OlharParaTela");
    }
}
