using System.Collections.Generic;
//using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TaskListLoader : MonoBehaviour
{
    public TaskListManager taskListManager;
    public LevelTaskData[] allLevelTasks;

    public static HashSet<string> CurrentExpectedItemID = new HashSet<string>(); //for global item validation

    void Start()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        foreach(var data in allLevelTasks)
        {
            if (data.sceneName == currentScene)
            {
                taskListManager.LoadTasks(data);
                CurrentExpectedItemID = new HashSet<string>(data.expectedItemID);
                return;
            }
        }
        Debug.LogWarning("No task data found for this scene");
    }
}
