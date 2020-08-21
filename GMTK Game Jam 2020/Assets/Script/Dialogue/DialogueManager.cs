using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [HideInInspector] public TextMeshProUGUI nameTxt;
    [HideInInspector] public TextMeshProUGUI dialogueTxt;
    private Animator anim;
    public static DialogueManager instance;
    public bool currentDialogueFinished = false;
    public string currentDialogueName = "";
    private void Awake()
    {
        if (instance == null)
            instance = this;

        sentences = new Queue<string>();
        anim = GameObject.Find("DialogueBox").GetComponent<Animator>();
    }

    private void Update()
    {
        if (currentDialogueFinished)
            StopAllCoroutines();
    }

    private Queue<string> sentences;

    public void StartDialogue(Dialogue dialogue)
    {
        anim.SetBool("isOpen", true);
        currentDialogueFinished = false;
        nameTxt.text = dialogue.name;

        sentences.Clear();
        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));

        //gambiarra perddoa deus mas preciso terminar logo
        if (sentence == "Don’t deny that you noticed the A.I. inside you attempting to mold you a personality. Don’t deny that you felt someone controlling your movements, hurting you to the point of losing control over your own body.")
        {
            LevelBOSSManager lvBoss = FindObjectOfType<LevelBOSSManager>();
            lvBoss.EventoOlharParaTela();
        }
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueTxt.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            AudioManager.instance.PlayByName("TypeText");
            dialogueTxt.text += letter;
            yield return new WaitForSeconds(.05f);
        }
    }

    private void EndDialogue()
    {
        anim.SetBool("isOpen", false);
        currentDialogueFinished = true;
    }
}
