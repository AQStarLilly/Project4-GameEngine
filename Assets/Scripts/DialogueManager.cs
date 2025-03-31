using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public GameObject dialogueUI;
    public TMP_Text dialogueText;
    public Button nextButton;

    private Queue<string> dialogue;    

    // Start is called before the first frame update
    void Start()
    {
        dialogue = new Queue<string>();
        dialogueUI.SetActive(false);
    }

    public void StartDialogue(string[] sentences)
    {
        Time.timeScale = 0;
        Singleton.Instance.player.canMove = false;
        dialogue.Clear();
        dialogueUI.SetActive(true);

        foreach(string currentString in sentences)
        {
            dialogue.Enqueue(currentString);
        }

        foreach(string sentence in dialogue)
        {
            Debug.Log(sentence);  //Debug to console
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if(dialogue.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = dialogue.Dequeue();
        dialogueText.text = sentence;
    }

    void EndDialogue()
    {
        Time.timeScale = 1;
        Singleton.Instance.player.canMove = true;
        dialogueUI.SetActive(false);
    }
}
