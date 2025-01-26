using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class EnemyManager : MonoBehaviour, IObserver
{
    [SerializeField] Subject gameManager;
    [Header("Enemies")]
    [SerializeField] List<GameObject> enemies;

    [SerializeField] Transform PlayerPos;

    private void SpawnEnemy()
    {
        Vector2 spawnPos = GetSpawnPos();

        for (int i = 0; i < enemies.Count; i++)
        {

        }
    }

    public void OnNotify(Action action)
    {
        if (action == Action.Eat)
        {
            SpawnEnemy();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            Vector2 test = GetSpawnPos();

            Debug.Log($"pos at x: {test.x}  y: {test.y}");
        }
    }

    private Vector2 GetSpawnPos()
    {
        // Get the direction for spawning
        int direction = Random.Range(0, 4);

        // Get the player's position
        float x = PlayerPos.transform.position.x;
        float y = PlayerPos.transform.position.y;

        // Get the camera's position, field of view (FOV), and aspect ratio
        Camera cam = Camera.main;
        float fov = cam.fieldOfView;
        float aspect = cam.aspect;
        float distanceToCamera = Mathf.Abs(PlayerPos.transform.position.z - cam.transform.position.z);

        // Calculate the height and width of the camera's view at the player's distance
        float height = 2f * Mathf.Tan(fov * 0.5f * Mathf.Deg2Rad) * distanceToCamera;
        float width = height * aspect;

        // Adjust the spawn position based on the camera's size and the player's position
        switch (direction)
        {
            case 0:  // Spawn to the left of the camera
                return new Vector2(x - width - 1f, y);  // 1f is added to ensure it's outside the camera's view
            case 1:  // Spawn to the right of the camera
                return new Vector2(x + width + 1f, y);  // 1f is added to ensure it's outside the camera's view
            case 2:  // Spawn above the camera
                return new Vector2(x, y + height + 1f);  // 1f is added to ensure it's outside the camera's view
            case 3:  // Spawn below the camera
                return new Vector2(x, y - height - 1f);  // 1f is added to ensure it's outside the camera's view
            default:  // Default to player's position
                return new Vector2(x, y);
        }
    }
    private void OnEnable()
    {
        gameManager.AddObserver(this);
    }

    private void OnDisable()
    {
        gameManager.RemoveObserver(this);
    }
}