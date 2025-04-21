using UnityEngine;

[CreateAssetMenu(fileName = "LevelTaskData", menuName = "Tasks/Level Task Data")]
public class LevelTaskData : ScriptableObject
{
    public string sceneName;
    [TextArea] public string[] tasks;
    public string[] expectedItemID; //Id for the item that can be collected
    
}
