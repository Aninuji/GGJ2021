using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AlmaPutrida : MonoBehaviour
{
    public float range;
    public GameObject almaInstance;
    private GameObject _player;
    private NavMeshAgent _navMesh;
    [SerializeField]
    private float _rateOfSpawn;

    [SerializeField]
    private bool _isSpawner = false;
    private List<GameObject> childrens;

    private void Start()
    {
        childrens = new List<GameObject>();
        _player = GameObject.Find("Player");
        _navMesh = GetComponent<NavMeshAgent>();

        if (_isSpawner)
        {
            InitialSpawn();
        }
    }

    private void Update()
    {
        if (Vector3.Distance(_player.transform.position, transform.position) <= range)
        {
            _navMesh.destination = _player.transform.position;
        }
    }

    private void InitialSpawn()
    {
        childrens.Add(Instantiate(almaInstance, new Vector3(transform.position.x + 1, transform.position.y, transform.position.z), Quaternion.identity, transform));
        childrens.Add(Instantiate(almaInstance, new Vector3(transform.position.x - 1, transform.position.y, transform.position.z), Quaternion.identity, transform));
        childrens.Add(Instantiate(almaInstance, new Vector3(transform.position.x, transform.position.y, transform.position.z + 1), Quaternion.identity, transform));
        childrens.Add(Instantiate(almaInstance, new Vector3(transform.position.x, transform.position.y, transform.position.z - 1), Quaternion.identity, transform));

        foreach (GameObject c in childrens)
        {
            c.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
        }

        StartCoroutine(SpawnNewChild());
    }

    public IEnumerator SpawnNewChild()
    {
        yield return new WaitForSeconds(_rateOfSpawn);
        GameObject newEnemie = Instantiate(almaInstance, new Vector3(transform.position.x, transform.position.y, transform.position.z - 1), Quaternion.identity, transform);
        newEnemie.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
        childrens.Add(newEnemie);
        StartCoroutine(SpawnNewChild());
    }
}
