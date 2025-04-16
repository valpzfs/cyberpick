using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Title : MonoBehaviour
{
    public float delay = 0.05f;
    public string fullText;
    private string currentText = "";
    public TextMeshProUGUI textcomponent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(ShowText());
 
    }

    IEnumerator ShowText()
    {
        //Type character by character
        for(int i = 0; i < fullText.Length; i++)
        {
            currentText = fullText.Substring(0, i + 1);
            textcomponent.text = currentText;
            yield return new WaitForSeconds(delay);
        }
    }

    void Update()
    {
        //Wobbly text
        textcomponent.ForceMeshUpdate();
        var textInfo = textcomponent.textInfo;

        for (int i = 0; i < textInfo.characterCount; ++i)
        {
            var charInfo = textInfo.characterInfo[i];

            if(!charInfo.isVisible)
            {
                continue;
            }
            var verts = textInfo.meshInfo[charInfo.materialReferenceIndex].vertices;

            for (int j = 0; j < 4; ++j)
            {
                var origin = verts[charInfo.vertexIndex + j];
                verts[charInfo.vertexIndex + j] = origin + new Vector3(0, Mathf.Cos(Time.time*-2f + origin.x*0.01f)* 5f, 0);

            }
        }
        for (int i = 0; i < textInfo.meshInfo.Length; ++i)
        {
           var meshInfo = textInfo.meshInfo[i];
           meshInfo.mesh.vertices = meshInfo.vertices;
           textcomponent.UpdateGeometry(meshInfo.mesh, i);
        }
    }
}
