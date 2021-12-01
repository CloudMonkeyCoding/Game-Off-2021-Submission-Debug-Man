using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    public Dialogue dialogue;

    public TextMeshProUGUI dialogueText;
    private Queue<string> sentences;

    private bool sentenceFinished;

    private string sentence; 

    void Start()
    {
        sentences = new Queue<string>();
        StartDialogue();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
            sentenceCheck();
    }

    public void sentenceCheck()
    {
        if (sentenceFinished)
        {
            DisplayNextSentence();
        }
        else
        {
            StopAllCoroutines();
            dialogueText.text = sentence;
            sentenceFinished = true;
        }
    }

    public void StartDialogue()
    {
        foreach (string line in dialogue.sentences)
        {
            sentences.Enqueue(line);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        sentence = sentences.Dequeue();
        StopAllCoroutines();
        sentenceFinished = false;

        StartCoroutine(TypeSentence());
    }

    IEnumerator TypeSentence()
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
        sentenceFinished = true;
    }

    public void EndDialogue()
    {
        SceneManager.LoadScene(2);
    }
}