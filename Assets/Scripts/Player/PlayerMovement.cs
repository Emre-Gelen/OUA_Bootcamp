using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.Windows;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerInputs))]
public class PlayerMovement : MonoBehaviour
{
    public float MoveSpeed = 2.0f;
    public float CarryingMoveSpeed = 1.2f;
    public float SprintSpeed = 5.0f;
    public float SpeedChangeRate = 10.0f;

    [Range(0.0f, 0.3f)]
    public float RotationSmoothTime = 0.12f;

    [Space(10)]
    public float JumpHeight = 1.2f;
    public float Gravity = -15.0f;

    [Space(10)]
    public float JumpTimeout = 0.6f;
    public float FallTimeout = 0.15f;

    [Space(10)]
    public float GroundedOffset = -0.14f;
    public float GroundedRadius = 0.28f;
    public LayerMask GroundLayers;

    // animation IDs
    private int _animIDSpeed;
    private int _animIDGrounded;
    private int _animIDJump;
    private int _animIDFreeFall;
    private int _animIDMotionSpeed;
    private int _animIDIsCarrying;

    private float _speed;
    private float _animationBlend;
    private float _targetRotation = 0.0f;
    private float _rotationVelocity;
    private float _verticalVelocity;
    private float _terminalVelocity = 53.0f;

    // timeout deltatime
    private float _jumpTimeoutDelta;
    private float _fallTimeoutDelta;

    private bool Grounded = true;

    private PlayerInputs _playerInputs;
    private CharacterController _characterController;
    private Animator _characterAnimator;
    private GameObject _mainCamera;

    private void Awake()
    {
        // get a reference to our main camera
        if (_mainCamera == null)
        {
            _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        }
    }

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _playerInputs = GetComponent<PlayerInputs>();
        _characterAnimator = GetComponent<Animator>();

        _jumpTimeoutDelta = JumpTimeout;
        _fallTimeoutDelta = FallTimeout;

        AssignAnimationIDs();
    }

    private void Update()
    {
        JumpAndGravity();
        GroundedCheck();
        Move();
    }

    private void AssignAnimationIDs()
    {
        _animIDSpeed = Animator.StringToHash("Speed");
        _animIDGrounded = Animator.StringToHash("Grounded");
        _animIDJump = Animator.StringToHash("Jump");
        _animIDFreeFall = Animator.StringToHash("Falling");
        _animIDMotionSpeed = Animator.StringToHash("MotionSpeed");
        _animIDIsCarrying = Animator.StringToHash("IsCarrying");
    }

    private void GroundedCheck()
    {
        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z);
        Grounded = Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers, QueryTriggerInteraction.Ignore);

        _characterAnimator.SetBool(_animIDGrounded, Grounded);
    }

    private void Move()
    {
        float targetSpeed = _playerInputs.IsPickup() ? CarryingMoveSpeed : (_playerInputs.IsSprinting() ? SprintSpeed : MoveSpeed);

        if (_playerInputs.GetMove() == Vector2.zero) targetSpeed = 0.0f;

        float currentHorizontalSpeed = new Vector3(transform.InverseTransformDirection(_characterController.velocity).x, 0.0f, transform.InverseTransformDirection(_characterController.velocity).z).magnitude;
        float speedOffset = 0.1f;
        float inputMagnitude = _playerInputs.analogMovement ? _playerInputs.GetMove().magnitude : 1f;

        if (currentHorizontalSpeed < targetSpeed - speedOffset || currentHorizontalSpeed > targetSpeed + speedOffset)
        {
            _speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude,
                    Time.deltaTime * SpeedChangeRate);

            _speed = Mathf.Round(_speed * 1000f) / 1000f;
        }
        else
        {
            _speed = targetSpeed;
        }

        _animationBlend = Mathf.Lerp(_animationBlend, targetSpeed, Time.deltaTime * SpeedChangeRate);
        if (_animationBlend < 0.01f) _animationBlend = 0f;

        if (_playerInputs.GetMove() != Vector2.zero)
        {
            Vector3 inputDirection = _playerInputs.GetPushPull() ? Vector3.Scale(_playerInputs.GetMovementAxis(), new Vector3(_playerInputs.GetMove().y, 0.0f, _playerInputs.GetMove().y)) : new Vector3(_playerInputs.GetMove().x, 0.0f, _playerInputs.GetMove().y).normalized;

            Debug.Log(inputDirection);

            _targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg +
                                  _mainCamera.transform.eulerAngles.y;
            float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _rotationVelocity,
                RotationSmoothTime);

            if (!_playerInputs.GetPushPull())
                transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
        }

        Vector3 targetDirection = Quaternion.Euler(0.0f, _targetRotation, 0.0f) * Vector3.forward;
        _characterController.Move(targetDirection.normalized * (_speed * Time.deltaTime) + new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);

        _characterAnimator.SetFloat(_animIDSpeed, _animationBlend);
        _characterAnimator.SetFloat(_animIDMotionSpeed, inputMagnitude);
        _characterAnimator.SetBool(_animIDIsCarrying, _playerInputs.IsPickup());
    }

    private void JumpAndGravity()
    {
        if (Grounded)
        {
            _fallTimeoutDelta = FallTimeout;

            _characterAnimator.SetBool(_animIDJump, false);
            _characterAnimator.SetBool(_animIDFreeFall, false);

            if (_verticalVelocity < 0.0f)
            {
                _verticalVelocity = -2f;
            }

            if (_playerInputs.IsJumping() && _jumpTimeoutDelta <= 0.0f)
            {
                _verticalVelocity = Mathf.Sqrt(JumpHeight * -2f * Gravity);
                _characterAnimator.SetBool(_animIDJump, true);
            }

            if (_jumpTimeoutDelta >= 0.0f)
            {
                _jumpTimeoutDelta -= Time.deltaTime;
            }
        }
        else
        {
            _jumpTimeoutDelta = JumpTimeout;

            if (_fallTimeoutDelta >= 0.0f)
            {
                _fallTimeoutDelta -= Time.deltaTime;
            }
            else
            {
                _characterAnimator.SetBool(_animIDFreeFall, true);
            }

            _playerInputs.JumpInput(false);
        }

        if (_verticalVelocity < _terminalVelocity)
        {
            _verticalVelocity += Gravity * Time.deltaTime;
        }
    }
}