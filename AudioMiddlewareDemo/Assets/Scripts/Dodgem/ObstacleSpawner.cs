using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TriggerOnBeat))]
public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> _obstacles = new List<GameObject>();
    [SerializeField] private List<Transform> _spawnPoints = new List<Transform>();

    [SerializeField][Range(0,1)] private float _spawnChance = 0.5f;

    private TriggerOnBeat _beatTrigger;

    protected void Awake()
    {
        _beatTrigger = GetComponent<TriggerOnBeat>();
    }

    private bool ShouldSpawn => Random.Range(0f, 1f) < _spawnChance;

    public void SpawnRandomObstacle()
    {
        if (!ShouldSpawn) return;

        Transform spawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Count)];
        GameObject obstaclePrefab = _obstacles[Random.Range(0, _obstacles.Count)];

        GameObject newObstacle = Instantiate(obstaclePrefab, spawnPoint.position, spawnPoint.rotation, spawnPoint);

        if (newObstacle.TryGetComponent(out IBeatListener beatListener))
            beatListener.SetBeatTrigger(_beatTrigger);
    }
}
