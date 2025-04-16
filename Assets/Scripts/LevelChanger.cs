using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    [SerializeField]
    private LevelConnections _connection;

    [SerializeField]

    private string _targetSceneName;

    [SerializeField]
    private Transform _spawnPoint;

    private void Start()
    {
        if (_connection == LevelConnections.ActiveConnection)
        {
            FindAnyObjectByType<Player>().transform.position = _spawnPoint.position;
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        var player = other.collider.GetComponent<Player>();
        if (player != null)
        {
            LevelConnections.ActiveConnection = _connection;
            SceneManager.LoadScene(_targetSceneName);

        }
    
    }
}