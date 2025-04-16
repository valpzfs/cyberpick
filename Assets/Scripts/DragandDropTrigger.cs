using UnityEngine;
using UnityEngine.SceneManagement;

public class DragandDropTrigger : MonoBehaviour
{
void OnMouseDown()
{
    Debug.Log("Se hizo clic en objeto, cargando escena...");
    SceneManager.LoadScene("DragAndDropMiniGame");
}
}
