using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    private Queue<string> sentences;

    //Canvas vars//
    public Text nameText;
    public Text dialogueText;
    public GameObject box;
    public Image iconbox;

    public bool finishDialogue = false;

    void Start()
    {
        sentences = new Queue<string>();
    }
    public void StartDialogue(Dialogue dialogue)
    {
        nameText.text = dialogue.nameNpc;
        iconbox.sprite = dialogue.icon;

        finishDialogue =false;

        box.SetActive(true);

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
        dialogueText.text = sentence;
    }
    void EndDialogue()
    {
        finishDialogue = true;

        box.SetActive(false);

        Debug.Log("Finaliza dialogo");
    }
}
