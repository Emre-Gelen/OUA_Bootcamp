using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCController : MonoBehaviour
{
    [SerializeField] private List<Transform> _areaCorners;

    [Space(10)]
    [SerializeField] private float _sightRadius = 5f;
    [SerializeField] private LayerMask _playerLayerMask;
    [SerializeField] private Transform _player;

    [Space(10)]
    [SerializeField] private float _checkCornerDistance = 1.5f;

    private int _currentCornerIndex = 0;
    private NavMeshAgent _npcAgent;

    private void Awake()
    {
        _npcAgent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        _npcAgent.SetDestination(GetNextCorner());
    }

    void Update()
    {
        if (!IsPlayerInSightRange())
        {
            _npcAgent.SetDestination(IsArrivedCorner() ? GetNextCorner() : _areaCorners[_currentCornerIndex % _areaCorners.Count].transform.position);
        }
        else
        {
            _npcAgent.SetDestination(_player.position);
        }
    }

    private bool IsPlayerInSightRange()
    {
        return Physics.CheckSphere(transform.position, _sightRadius, _playerLayerMask);
    }

    private bool IsArrivedCorner()
    {
        return Mathf.Abs(Vector3.Distance(transform.position, _areaCorners[_currentCornerIndex % _areaCorners.Count].transform.position)) < _checkCornerDistance;
    }

    private Vector3 GetNextCorner()
    {
        return _areaCorners[++_currentCornerIndex % _areaCorners.Count].transform.position;
    }
}
