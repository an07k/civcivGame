using System;
using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public event Action OnPlayerJumped;

    public event Action<PlayerState> OnPlayerStateChanged;

    [Header("References")]
    [SerializeField] private Transform _orientationTransform;
    private PlayerStateController _stateController;
    private Rigidbody _playerRigidbody;
    [Header("Movement Stg")]
    [SerializeField] private Vector3 _baseScale;
    [SerializeField] private float _movementSpeed;

    [SerializeField] private KeyCode _movementKey;

    [Header("Slide Stg")]
    [SerializeField] private KeyCode _slideKey;
    [SerializeField] private float _slideMultiplier;
    [SerializeField] private float _slideDamping;

    [Header("Jump Stg")]
    [SerializeField] private float _jumpForce;
    [SerializeField] private KeyCode _jumpKey;
    [SerializeField] private bool _canJump;
    [SerializeField] private float _jumpCooldown;
    [SerializeField] private float _airDamping;
    [SerializeField] private float _airMultiplier;

    [Header("Ground Check")]
    [SerializeField] private float _playerHeight;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private float _groundDamping;

    private float _horizontalInput, _verticalInput;
    private bool _isSliding;
    private Vector3 _movementDirection;
    private float _startingMovementSpeed, _startingJumpForce;



    private void Awake()
    {

        _baseScale = transform.localScale;
        _startingMovementSpeed = _movementSpeed;
        _startingJumpForce = _jumpForce;

        _stateController = GetComponent<PlayerStateController>();
        _playerRigidbody = GetComponent<Rigidbody>();
        _playerRigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        _playerRigidbody.freezeRotation = true;
        _playerRigidbody.interpolation = RigidbodyInterpolation.Interpolate;


    }



    private void Update()
    {
        SetPlayerDamping();
        SetStates();
        SetInputs();
        LimitPlayerSpeed();
    }
    private void FixedUpdate()
    {
        SetPlayerDamping();
        SetPlayerMovement();
    }
    private void SetInputs()
    {
        _horizontalInput = Input.GetAxis("Horizontal");
        _verticalInput = Input.GetAxis("Vertical");

        if (Input.GetKey(_jumpKey) && _canJump && IsGrounded())
        {
            // Zıplama işlemi gerçekleşecek.
            _canJump = false;
            SetPlayerJump();
            Invoke(nameof(ResetJumping), _jumpCooldown);
        }

        if (Input.GetKeyDown(_slideKey))
        {
            _isSliding = true;
        }
        else if (Input.GetKeyDown(_movementKey))
        {
            _isSliding = false;
        }
    }
    private void SetStates()
    {
        var movementDirection = GetMovementDirection();
        var isGrounded = IsGrounded();
        var currentState = _stateController.GetCurrentState();
        var isSliding = IsSliding();
        var newState = currentState switch
        {
            _ when movementDirection == Vector3.zero && isGrounded && !isSliding => PlayerState.Idle,
            _ when movementDirection != Vector3.zero && isGrounded && !isSliding => PlayerState.Move,
            _ when movementDirection != Vector3.zero && isGrounded && isSliding => PlayerState.Slide,
            _ when movementDirection == Vector3.zero && isGrounded && isSliding => PlayerState.SlideIdle,
            _ when !_canJump && !isGrounded => PlayerState.Jump,
            _ => currentState
        };

        if (newState != currentState)
        {
            _stateController.ChangeState(newState);
            OnPlayerStateChanged?.Invoke(newState);
        }
    }

    private void LimitPlayerSpeed()
    {
        Vector3 flatVelocity = new Vector3(_playerRigidbody.linearVelocity.x, 0f, _playerRigidbody.linearVelocity.z);

        if (flatVelocity.magnitude > _movementSpeed)
        {
            Vector3 limitedVelocity = flatVelocity.normalized * _movementSpeed;
            _playerRigidbody.linearVelocity = new Vector3(limitedVelocity.x, _playerRigidbody.linearVelocity.y, limitedVelocity.z);
        }
    }
    private void SetPlayerMovement()
    {
        _movementDirection = _orientationTransform.forward * _verticalInput +
        _orientationTransform.right * _horizontalInput;

        float forceMultiplier = _stateController.GetCurrentState() switch
        {
            PlayerState.Move => 1f,
            PlayerState.Slide => _slideMultiplier,
            PlayerState.Jump => _airMultiplier,
            _ => 1f
        };

        _playerRigidbody.AddForce(_movementDirection.normalized * _movementSpeed * forceMultiplier, ForceMode.Force);

    }

    private void SetPlayerDamping()
    {
        _playerRigidbody.linearDamping = _stateController.GetCurrentState() switch
        {
            PlayerState.Move => _groundDamping,
            PlayerState.Slide => _slideDamping,
            PlayerState.Jump => _airDamping,
            _ => _playerRigidbody.linearDamping
        };
    }
    private void SetPlayerJump()
    {
        OnPlayerJumped?.Invoke();

        _playerRigidbody.linearVelocity = new Vector3(_playerRigidbody.linearVelocity.x, 0f, _playerRigidbody.linearVelocity.z);
        _playerRigidbody.AddForce(transform.up * _jumpForce, ForceMode.Impulse);
    }

    private void ResetJumping()
    {
        _canJump = true;
    }

    #region Helper Functions
    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, _playerHeight * 0.5f + 0.2f, _groundLayer);
    }

    private Vector3 GetMovementDirection()
    {
        return _movementDirection.normalized;
    }

    private bool IsSliding()
    {
        return _isSliding;
    }
    public void SetMovementSpeed(float speed, float duration)
    {
        _movementSpeed += speed;
        Invoke(nameof(ResetMovementSpeed), duration);
    }

    private void ResetMovementSpeed()
    {
        _movementSpeed = _startingMovementSpeed;
    }

    public void SetJumpForce(float force, float duration)
    {
        _jumpForce += force;
        Invoke(nameof(ResetJumpForce), duration);
    }
    private float _multiply2;
    public void IncreaseScale(float multiply, float duration)
    {
        _multiply2 = multiply;
        _baseScale.x = _baseScale.x * multiply;
        _baseScale.y = _baseScale.y * multiply;
        _baseScale.z = _baseScale.z * multiply;
        transform.localScale = _baseScale;
        Invoke(nameof(ResetScaleFake), duration);
    }


    private void ResetScaleFake()
    {
        ResetScale(_multiply2);
    }

    private void ResetScale(float multiplier)
    {
        _baseScale.x = _baseScale.x / multiplier;
        _baseScale.y = _baseScale.y / multiplier;
        _baseScale.z = _baseScale.z / multiplier;
        transform.localScale = _baseScale;
    }
    private void ResetJumpForce()
    {
        _jumpForce = _startingJumpForce;
    }
    #endregion

    public Rigidbody GetPlayerRigidbody()
    {
        return _playerRigidbody;
    }
}
