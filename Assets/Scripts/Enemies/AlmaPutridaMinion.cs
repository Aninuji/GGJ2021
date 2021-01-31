﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AlmaPutridaMinion : MonoBehaviour
{
    public float range;
    private GameObject _player;
    private NavMeshAgent _navMesh;

    private void Start()
    {
        _player = WorldGenerator.Instance._playerInstance;
        _navMesh = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (Vector3.Distance(_player.transform.position, transform.position) <= range)
        {
            _navMesh.destination = _player.transform.position;
        }
    }
}