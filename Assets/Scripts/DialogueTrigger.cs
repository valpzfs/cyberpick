using System.Collections.Generic;
using UnityEngine;

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
    

    public void TriggerDialogue()
    {
         if (DialogueManager.Instance == null)
    {
        Debug.LogError("Error: DialogueManager.Instance es null");
        return;
    }

    Debug.Log("TriggerDialogue() ejecutado correctamente.");
    GameManager.instance.SumPoints(5);
    DialogueManager.Instance.StartDialogue(dialogue);
    }
private void OnTriggerEnter2D(Collider2D collision)
{
    Debug.Log("Colisión detectada con: " + collision.gameObject.name);
    
    if (collision.CompareTag("Player") && !DialogueManager.Instance.isDialogueActive)
    {
        Debug.Log("El jugador ha activado el diálogo.");
        GameManager.instance.TalkedtoSupervisor = true;
        TriggerDialogue();
        
    }
}

}
