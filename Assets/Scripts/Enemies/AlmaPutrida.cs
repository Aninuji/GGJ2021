using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AlmaPutrida : MonoBehaviour
{
    public float range;
    public GameObject almaInstance;
    [SerializeField]
    private float _rateOfSpawn = 2;
    [SerializeField]
    private bool _isSpawner = true;
    [SerializeField]
    private bool _canSpawn = false;
    private GameObject _player;
    private NavMeshAgent _navMesh;
    public List<GameObject> childrens;

    private void Start()
    {
        childrens = new List<GameObject>();
        _player = WorldGenerator.Instance._playerInstance;
        _navMesh = GetComponent<NavMeshAgent>();

        SpawnInitialChilds();

    }

    private void Update()
    {
        
        if (Vector3.Distance(_player.transform.position, transform.position) <= range)
        {
            _navMesh.destination = _player.transform.position;
            _canSpawn = true;
        }else{
            _canSpawn = false;
        }
        

    }

    public void SpawnInitialChilds()
    {
        for (int i = -1; i < 2; i += 2)
        {
            for (int j = -1; j < 2; j += 2)
            {
                GameObject newEnemie = Instantiate(almaInstance, new Vector3(transform.position.x + i, transform.position.y, transform.position.z + j), Quaternion.identity, transform);
                childrens.Add(newEnemie);
            }
        }

        StartCoroutine(SpawnNewChild());
    }

    public IEnumerator SpawnNewChild()
    {
        yield return new WaitForSeconds(_rateOfSpawn);
        Debug.Log("Trying to spawn");
        if(_canSpawn)
        {
            Debug.Log("spawning");
            GameObject newEnemie = Instantiate(almaInstance, new Vector3(transform.position.x, transform.position.y, transform.position.z - 1), Quaternion.identity, transform);
            newEnemie.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
        }
        StartCoroutine(SpawnNewChild());
        
    }
}
