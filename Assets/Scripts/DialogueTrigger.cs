using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using UnityEngine;
//using UnityEngine.UI;

[System.Serializable]
public class DialogueCharacter
{
    public string name;
    public Sprite icon;
}

[System.Serializable]
public class DialogueLine
{
    public DialogueCharacter character;
    [TextArea(3, 10)]
    public string line;
}

[System.Serializable]
public class Dialogue
{
    public List<DialogueLine> dialogueLines = new List<DialogueLine>();
}

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public DialogueManager dialogueManager; //Assign from Inspector
    public GameObject dialogueCanvas;
    void Start()
    {
        if (dialogueCanvas != null)
            dialogueCanvas.SetActive(false);
    }


    public void TriggerDialogue()
    {
        Debug.Log("TriggerDialogue ejectuado");
        GameManager.instance.SumPoints(5);
        dialogueManager.StartDialogue(dialogue, dialogueCanvas);
    }
    // {
    //      if (DialogueManager.Instance == null)
    // {
    //     Debug.LogError("Error: DialogueManager.Instance es null");
    //     return;
    // }

    // Debug.Log("TriggerDialogue() ejecutado correctamente.");
    // GameManager.instance.SumPoints(5);
    // DialogueManager.Instance.StartDialogue(dialogue,dialogueCanvas);
    // }
private void OnTriggerEnter2D(Collider2D collision)
{
    if(collision.CompareTag("Player") && !dialogueManager.isDialogueActive)
    {
        Debug.Log("Jugador inicio dialogo");
        GameManager.instance.TalkedtoSupervisor = true;
        TriggerDialogue();
    }
    // Debug.Log("Colisión detectada con: " + collision.gameObject.name);
    
    // if (collision.CompareTag("Player") && !DialogueManager.Instance.isDialogueActive)
    // {
    //     Debug.Log("El jugador ha activado el diálogo.");
    //     GameManager.instance.TalkedtoSupervisor = true;
    //     TriggerDialogue();
        
    // }
}

}
