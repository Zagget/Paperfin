using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [Header("Enemies")]
    [SerializeField] List<GameObject> smallEnemies;
    [SerializeField] List<GameObject> mediumEnemies;
    [SerializeField] List<GameObject> largeEnemies;

    [SerializeField] Transform PlayerPos;

    float cameraWidth;
    float cameraHeight;

    void Start()
    {
        cameraHeight = Camera.main.orthographicSize * 2f;
        cameraWidth = cameraHeight * 2f;
    }

    private void EnemySpawner()
    {

    }

    private Vector2 GetSpawnPos()
    {
        int direction = Random.Range(0, 4);

        float x = PlayerPos.transform.position.x;
        float y = PlayerPos.transform.position.y;

        switch (direction)
        {
            case 0:
                return new Vector2(x - 6, y);
            case 1:
                return new Vector2(x + 6, y);
            case 2:
                return new Vector2(x, y + 6);
            case 3:
                return new Vector2(x, y - 6);
            default:
                return new Vector2(x, y);
        }
    }
}