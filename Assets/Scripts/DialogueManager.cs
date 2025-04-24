using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    //public static DialogueManager Instance;

    public Image characterIcon;
    public TextMeshProUGUI characterName;
    public TextMeshProUGUI dialogueArea;
    public Animator animator;

    private Queue<DialogueLine> lines = new Queue<DialogueLine>();
    
    public bool isDialogueActive = false;
    private GameObject dialogueCanvas;
    public float typingSpeed = 0.2f;


    // private void Awake()
    // {
    //     if (Instance == null)
    //         Instance = this;

    //     lines = new Queue<DialogueLine>();

    // }

    public void StartDialogue(Dialogue dialogue, GameObject canvas)
    {
        isDialogueActive = true;
        dialogueCanvas = canvas;
        dialogueCanvas.SetActive(true);
        if(animator != null)animator.SetBool("IsTalking", true);

        lines.Clear();

        foreach (DialogueLine dialogueLine in dialogue.dialogueLines)
        {
            lines.Enqueue(dialogueLine);
        }

        DisplayNextDialogueLine();
    }

    public void DisplayNextDialogueLine()
    {
        if (lines.Count == 0)
        {
            EndDialogue();
            return;
        }

        DialogueLine currentLine = lines.Dequeue();

        characterIcon.sprite = currentLine.character.icon;
        characterName.text = currentLine.character.name;

        StopAllCoroutines();
        StartCoroutine(TypeSentence(currentLine));
    }

    IEnumerator TypeSentence(DialogueLine dialogueLine)
    {
        dialogueArea.text = "";
        foreach (char letter in dialogueLine.line.ToCharArray())
        {
            dialogueArea.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    void EndDialogue()
    {
        isDialogueActive = false;
        
        // Desactivamos la animación de diálogo (regresamos a Idle)
        if(animator != null) animator.SetBool("IsTalking", false);

        // Ocultamos el Canvas
        if(dialogueCanvas != null)
            dialogueCanvas.SetActive(false);
    }
}

