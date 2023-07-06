using System.Collections;
using System.Collections.Generic;
using Cinemachine.Utility;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectPush : MonoBehaviour
{
    public float PullingPushingMoveSpeed = 1.3f;
    public float pushDistance = 0.6f;

    [SerializeField] private float pushForce;

    private Movable _lastMovableObject;

    private Transform _lastMovableObjectParent;
    private PlayerInputs _playerInputs;
    private CharacterController _characterController;
    private GameObject _mainCamera;

    private void Awake()
    {
        if (_mainCamera == null)
        {
            _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        }
    }

    private void Start()
    {
        _playerInputs = GetComponent<PlayerInputs>();
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Vector3 direction = transform.forward.normalized;
        Ray ray = new Ray(transform.position + new Vector3(0, .7f, 0), direction * pushDistance);

        GrabPoint _closestGrabPoint = default;
        if (Physics.Raycast(ray, out RaycastHit hit, pushDistance) && _playerInputs.GetPushPull())
        {
            Debug.Log(hit.transform.gameObject.name);
            _lastMovableObject = hit.collider.GetComponent<Movable>();

            GetClosestGrabPoint(ref _closestGrabPoint);

            if((_closestGrabPoint.transform.position - transform.position).magnitude > .2f) MoveToClosestGrabPoint(_closestGrabPoint.transform.position);
            else
            {
                _playerInputs.SetAxis(_closestGrabPoint.ImpactToMovement);
                _lastMovableObjectParent = _lastMovableObjectParent ?? _lastMovableObject.transform.parent;
                HandleForce(_lastMovableObject);
            }
        }
        else
        {
            HandleLeave();
        }
    }
    
    private void MoveToClosestGrabPoint(Vector3 target)
    {
        Vector3 offset = target - transform.position;

        offset = offset.normalized * PullingPushingMoveSpeed;
        _characterController.Move(offset * Time.deltaTime);
    }

    private void UpdateAnimation()
    {
        //TODO: Update character's animation when character pushes or pulls objects.
    }

    private void HandleForce(Movable movableObject)
    {
        movableObject.transform.SetParent(transform);
        movableObject.IsHeld = true;
    }

    private void HandleLeave()
    {
        if (_lastMovableObject != null)
        {
            _lastMovableObject.transform.SetParent(_lastMovableObjectParent != transform ? _lastMovableObjectParent : null);
            _lastMovableObject.IsHeld = false;
            _lastMovableObject = null;
        }

        _lastMovableObjectParent = null;
        _playerInputs.SetPullPushState(false);
    }

    private void GetClosestGrabPoint(ref GrabPoint closestGrabPoint )
    {
        foreach (GrabPoint grabPoint in _lastMovableObject.GetComponentsInChildren<GrabPoint>())
        {
            if (closestGrabPoint == default || Vector3.Distance(transform.position, closestGrabPoint.transform.position) > Vector3.Distance(transform.position, grabPoint.transform.position))
            {
                closestGrabPoint = grabPoint;
            }
        }
    }
}
