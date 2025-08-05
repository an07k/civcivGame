using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform _orientationTransform;
    private Rigidbody _playerRigidbody;
    [Header("Movement Stg")]
    [SerializeField] private float _movementSpeed;

    [Header("Jump Stg")]

    [SerializeField] private float _jumpForce;

    [SerializeField] private KeyCode _jumpKey;
    [SerializeField] private bool _canJump;
    [SerializeField] private float _jumpCooldown;

    [Header("Ground Check")]
    [SerializeField] private float _playerHeight;
    [SerializeField] private LayerMask _groundLayer;

    private float _horizontalInput, _verticalInput;

    private Vector3 _movementDirection;

    private void Awake()
    {
        _jumpKey = KeyCode.Space;
        _playerRigidbody = GetComponent<Rigidbody>();
        _playerRigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        _playerRigidbody.freezeRotation = true;
        _playerRigidbody.interpolation = RigidbodyInterpolation.Interpolate;
    }

    private void Update()
    {
        SetInputs();
    }
    private void FixedUpdate()
    {
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
    }

    private void SetPlayerMovement()
    {
        _movementDirection = _orientationTransform.forward * _verticalInput +
        _orientationTransform.right * _horizontalInput;

        _playerRigidbody.AddForce(_movementDirection.normalized * _movementSpeed, ForceMode.Force);
    }

    private void SetPlayerJump()
    {
        _playerRigidbody.linearVelocity = new Vector3(_playerRigidbody.linearVelocity.x, 0f, _playerRigidbody.linearVelocity.z);
        _playerRigidbody.AddForce(transform.up * _jumpForce, ForceMode.Impulse);
    }

    private void ResetJumping()
    {
        _canJump = true;
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, _playerHeight * 0.5f + 0.2f, _groundLayer);
    }
}
