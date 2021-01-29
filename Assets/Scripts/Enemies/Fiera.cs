using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Fiera : MonoBehaviour
{
    public float range;
    private GameObject _player;
    private NavMeshAgent _navMesh;


    private void Start()
    {
        _player = GameObject.Find("Player");
        _navMesh = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (Vector3.Distance(_player.transform.position, transform.position) <= range)
        {
            Debug.Log("Caminando");
            _navMesh.destination = _player.transform.position;
        }
    }
}
