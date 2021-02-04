using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(EnemyHealth), typeof(Animator))]
public class Fiera : MonoBehaviour
{
    public float range;
    public float ATTACKrange;

    private GameObject _player;
    private NavMeshAgent _navMesh;
    private EnemyHealth _health;

    private Animator _animator;

    private void Start()
    {
        _player = WorldGenerator.Instance._playerInstance;

        _navMesh = GetComponent<NavMeshAgent>();
        _health = GetComponent<EnemyHealth>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (_health.isAboutToDie) return;

        if (Vector3.Distance(_player.transform.position, transform.position) <= range)
        {

            Debug.Log("Caminando");
            _navMesh.destination = _player.transform.position;
            _animator.SetBool("Idle", false);
            _animator.SetBool("Run", true);
        }
        else
        {

            _animator.SetBool("Run", false);
            _animator.SetBool("Idle", true);

        }
    }
}
