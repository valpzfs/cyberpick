using UnityEngine;
using UnityEngine.SceneManagement;

public class MinigameManager : MonoBehaviour
{
    [SerializeField] private string[] minigameSceneNames;
    [SerializeField] private float delayBeforeLoad = 1f; // delay in seconds

    public void LoadRandomMinigame(GameObject triggerObject)
    {
        //Desactiva el collider
        Collider2D col = triggerObject.GetComponent<Collider2D>();
        if (col != null) col.enabled = false;
        
        //Esperar un poco antes de cambiar de escena
        Invoke(nameof(LoadScene), delayBeforeLoad);
    }

    private void LoadScene()
    {
        //Save current scene name to return to later
        PlayerPrefs.SetString("LastScene", SceneManager.GetActiveScene().name);
        PlayerPrefs.Save();

        int index = Random.Range(0, minigameSceneNames.Length);
        SceneManager.LoadScene(minigameSceneNames[index]);
    }
}

