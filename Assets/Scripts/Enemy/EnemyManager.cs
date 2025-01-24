using System.Collections.Generic;
using System.Numerics;
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

    private void GetSpawnPos()
    {
        int direction = Random.Range(0, 3);

        // Vector2 = new Vector2()

        // switch (direction)
        // {
        //     case 0:

        //     default:
        // }
    }
}