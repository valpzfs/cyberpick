using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Threading.Tasks;

public class TaskListManager : MonoBehaviour
{
    public GameObject taskItemPrefab;
    public Transform taskListContent;
    private List<GameObject> currentTasks = new List<GameObject>();

    public void LoadTasks(LevelTaskData data)
    {
        foreach(var item in currentTasks)
        {
            Destroy(item);
        }
        currentTasks.Clear();

        //Generate from data
        foreach(string task in data.tasks)
        {
            GameObject newTask = Instantiate(taskItemPrefab, taskListContent);
            newTask.GetComponent<TextMeshProUGUI>().text = "." + task;
            currentTasks.Add(newTask);
        }
    }
}
