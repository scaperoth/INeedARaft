using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject _itemToSpawn;
    [SerializeField]
    int _itemsToSpawn = 30;

    float range = 40f;
    int _spawnedItems = 0;
    List<Vector3> _rayPoints = new List<Vector3>();

    // Start is called before the first frame update
    void Start()
    {
        while (_spawnedItems < _itemsToSpawn)
        {
            SpawnItem();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if (_spawnedItems < _itemsToSpawn)
        //{
        //    SpawnItem();
        //}
    }

    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        for (int i = 0; i < 30; i++)
        {
            Vector3 randomPoint = center + Random.insideUnitSphere * range;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint + (Vector3.up * 20f), out hit, 1.0f, NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }
        }
        result = Vector3.zero;
        return false;
    }

    void SpawnItem()
    {
        Vector3 point;
        if (RandomPoint(transform.position, range, out point))
        {
            _spawnedItems++;
            var go = Instantiate(_itemToSpawn, transform);
            go.transform.position = point;
        }
    }
}
