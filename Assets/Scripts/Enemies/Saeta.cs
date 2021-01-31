﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;

public class Saeta : MonoBehaviour
{
    [Range(0, 1)]
    public float transparency;
    public GameObject proyectilePrefab;
    public float range;
    private GameObject _player;
    [SerializeField]
    private float _runSpeed = 4;
    [SerializeField]
    private float _rof;
    [SerializeField]
    private float _proyectileSpeed;
    private float _t = 0;
    private bool _shooting;
    private NavMeshAgent _navMesh;
    private MeshRenderer _meshRenderer;

    private EnemyHealth _health;

    private void Start()
    {
        _player = WorldGenerator.Instance._playerInstance;
        _navMesh = GetComponent<NavMeshAgent>();
        _meshRenderer = GetComponent<MeshRenderer>();
        _health = GetComponent<EnemyHealth>();
    }

    private void Update()
    {
        if (_health.isAboutToDie) return;

        Behaviour();

        Color actualColor = _meshRenderer.material.color;

        Color newColor = new Color(actualColor.r, actualColor.g, actualColor.b, transparency);

        _meshRenderer.material.color = newColor;
    }

    private void Behaviour()
    {
        Vector3 playerPos = _player.transform.position;
        if (Vector3.Distance(_player.transform.position, transform.position) <= range)
        {
            Debug.Log("Yeet");
            Vector3 desiredPos = new Vector3(transform.position.x - playerPos.x, transform.position.y, transform.position.z - playerPos.z);


            _navMesh.destination = transform.position + desiredPos;
            transform.LookAt(new Vector3(playerPos.x, transform.position.y, playerPos.z));
            LerpColor();

            Shoot();
        }
    }

    private void LerpColor()
    {
        _t += Time.deltaTime * 0.2f;

        transparency = Mathf.Lerp(1, 0.1f, _t);
    }

    private void Shoot()
    {
        if (!_shooting)
        {
            StartCoroutine(Fire());
        }
    }

    private IEnumerator Fire()
    {
        GameObject proyectile = Instantiate(proyectilePrefab, transform.position, Quaternion.identity);
        proyectile.GetComponent<Rigidbody>().AddRelativeForce(transform.forward * _proyectileSpeed, ForceMode.Impulse);
        _shooting = true;
        yield return new WaitForSeconds(_rof);
        Destroy(proyectile, 1);
        _shooting = false;
    }
}
