using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue[] dialogue;
    public string dialogueName;
    private int currentIndex = 0;
    public bool conversationEnded = false;
    private bool foi_utilizado = false;
    private bool control = false;

    private void Update()
    {
        AguardarFinish();

        if(currentIndex >= dialogue.Length)
        {
            if (foi_utilizado && !control)
            {
                OnConversationEnded();
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

    private void OnConversationEnded()
    {
        conversationEnded = true;
        control = true;
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

        if (dialogueName == "Segunda conversa Lv2")
        {
            Level02Manager lvl2 = FindObjectOfType<Level02Manager>();
            lvl2.MoverPlayerAuto();
        }

        if(dialogueName == "Intro conversation")
        {
            StartCoroutine(TrocarCena());
        }

        if (dialogueName == "Terceira conversa Lv3")
        {
            LevelManager lv = FindObjectOfType<LevelManager>();
            if(lv != null)
            {
                lv.GoToLevel(5);
            }
        }

        if(dialogueName == "Primeira conversa LvBOSS")
        {
            FindObjectOfType<BossIA>().canMove = true;
        }

        if (dialogueName == "Segunda conversa LvBOSS")
        {
            FindObjectOfType<BossIA>().canMove = true;
        }

        if(dialogueName == "Terceira conversa LvBOSS")
        {
            LevelBOSSManager lvBoss = FindObjectOfType<LevelBOSSManager>();
            StartCoroutine(lvBoss.SpriteFade(1, 5f));
        }

        if (dialogueName == "Primeira conversa LvCREDITOS")
        {
            AudioManager.instance.PlayByName("ExplosaoLevelBOSS");
            LevelCREDITOSManager lvCredito = FindObjectOfType<LevelCREDITOSManager>();
            StartCoroutine(lvCredito.TrocarMascara());
        }
    }

    IEnumerator TrocarCena()
    {
        yield return new WaitForSeconds(3f);

        LevelManager lvl = FindObjectOfType<LevelManager>();
        lvl.GoToLevel(2);
    }

}
