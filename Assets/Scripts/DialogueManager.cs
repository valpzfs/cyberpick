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
    public AudioSource soundEffect;

    private Queue<DialogueLine> lines = new Queue<DialogueLine>();
    
    public bool isDialogueActive = false;
    private GameObject dialogueCanvas;
    public float typingSpeed = 0.2f;



    public void StartDialogue(Dialogue dialogue, GameObject canvas)
    {
        isDialogueActive = true;
        dialogueCanvas = canvas;
        dialogueCanvas.SetActive(true);
        Transform box = dialogueCanvas.transform.Find("DialogueBox");
        //Actualizar las referencias
        characterIcon = box.transform.Find("SupervisorImg").GetComponent<Image>();
        characterName = box.transform.Find("SupervisorName").GetComponent<TextMeshProUGUI>();
        dialogueArea = box.transform.Find("Dialoguetxt").GetComponent<TextMeshProUGUI>();
        animator = box.GetComponent<Animator>();

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
        if(soundEffect != null)
            {
                soundEffect.loop = true;
                soundEffect.Play();
            }
        foreach (char letter in dialogueLine.line.ToCharArray())
        {
            dialogueArea.text += letter;
            yield return new WaitForSeconds(typingSpeed);

        }
        if (soundEffect != null)
        {
            soundEffect.Stop();
            soundEffect.loop = false;
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
            soundEffect.Stop();
    }
}

