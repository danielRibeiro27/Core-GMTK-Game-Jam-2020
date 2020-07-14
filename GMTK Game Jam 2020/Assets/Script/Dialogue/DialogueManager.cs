using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI nameTxt;
    public TextMeshProUGUI dialogueTxt;
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

    private Queue<string> sentences;

    private void Start()
    {
    }

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
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueTxt.text = "";

        foreach(char letter in sentence.ToCharArray())
        {
            dialogueTxt.text += letter;
            yield return null;
        }
    }

    private void EndDialogue()
    {
        anim.SetBool("isOpen", false);
        currentDialogueFinished = true;
    }
}
