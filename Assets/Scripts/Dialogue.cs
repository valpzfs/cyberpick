using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;
    private int index;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Solo se muestra si no se ha mostrado antes
    if (PlayerPrefs.GetInt("InitialDialogueShown", 0) == 0)
    {
        textComponent.text = string.Empty;
        StartDialogue();

        // Marca como mostrado
        PlayerPrefs.SetInt("InitialDialogueShown", 1);
        PlayerPrefs.Save();
    }
    else
    {
        gameObject.SetActive(false); // Oculta el diálogo si ya se mostró
    }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            if (textComponent.text == lines[index]){
                NextLine();
            }else{
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
        }
    }

    void StartDialogue(){
        index = 0;
        StartCoroutine(TypeLine());
    }
    IEnumerator TypeLine(){
        foreach (char c in lines[index].ToCharArray()){
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine(){
        if (index < lines.Length -1){
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }else{
            gameObject.SetActive(false);
        }
    }
}
