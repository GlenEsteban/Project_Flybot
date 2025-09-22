using UnityEngine;

public class GroundedAIController : MonoBehaviour {
    // Attributes and Variables
    [Header("Targeting")]
    [SerializeField] private bool _isFollowingTarget;
    [SerializeField] private Transform _target;
    [SerializeField] private float _targetingRatePerSec = 0.1f;
    [SerializeField] private float _followingDistance= 1f;

    [Header("Collider Detection")]
    [SerializeField] private bool _hasEdgeDetection = true;
    [SerializeField] private bool _hasHardStopOnEdgeDetection = true;
    [SerializeField] private bool _hasWallDetection = true;
    [SerializeField] private float _detectionRatePerSec = 0.1f;
    [SerializeField] private Collider2D _wallDetection;
    [SerializeField] private Collider2D _groundDetection;
    [SerializeField] private Collider2D _edgeDetection;

    // Movement Variables
    private Vector2 _moveDirection;

    // Cached References
    private Movement _movement;

    // Timers
    private float _targetingTimer;
    private float _detectionTimer;

    private void Awake() {
        _movement = GetComponent<Movement>();
    }

    private void Start() {
        _moveDirection = Vector2.right;
        _movement.SetMoveDirection(_moveDirection);
    }

    private void Update() {
        HandleGroundDetectionBehavior();

        _targetingTimer += Time.deltaTime;
        if (_isFollowingTarget && _targetingTimer > _targetingRatePerSec) {
            HandleFollowingBehavior();
        }

        if (!_isFollowingTarget && _detectionTimer > _detectionRatePerSec) {
            HandleEdgeDetectionBehavior();
            HandleWallDetectionBehavior();
        }
    }

    private void HandleFollowingBehavior() {
        float signedHorizontalDistanceToPlayer = _target.position.x - this.transform.position.x;
        float horizontalDistanceToPlayer = Mathf.Abs(signedHorizontalDistanceToPlayer);

        if (horizontalDistanceToPlayer > _followingDistance) {
            _moveDirection = signedHorizontalDistanceToPlayer > 0 ? Vector2.right : Vector2.left;
            _movement.SetMoveDirection(_moveDirection);
            _movement.FaceMoveDirection();
        }
        _targetingTimer = 0;
    }

    private void HandleGroundDetectionBehavior() {
        if (!_groundDetection.IsTouchingLayers(LayerMask.GetMask("Ground")) &&
            !_groundDetection.IsTouchingLayers(LayerMask.GetMask("Platform")) &&
            !_groundDetection.IsTouchingLayers(LayerMask.GetMask("NPC"))){

            _movement.SetCanMove(false);
        }
        else {
            _detectionTimer += Time.deltaTime;
            _movement.SetCanMove(true);
        }
    }

    private void HandleEdgeDetectionBehavior() {
        if (_hasEdgeDetection && !_edgeDetection.IsTouchingLayers(LayerMask.GetMask("Ground")) &&
            !_edgeDetection.IsTouchingLayers(LayerMask.GetMask("Platform")) &&
            !_edgeDetection.IsTouchingLayers(LayerMask.GetMask("NPC"))){

            if (_hasHardStopOnEdgeDetection) {
                _movement.HardStopMovement();
            }

            _movement.FlipHorizontalDirection();
            _movement.FaceMoveDirection();

            _detectionTimer = 0;
        }
    }

    private void HandleWallDetectionBehavior() {
        if (_hasWallDetection && _wallDetection.IsTouchingLayers(LayerMask.GetMask("Ground")) ||
            _wallDetection.IsTouchingLayers(LayerMask.GetMask("Platform")) ||
            _wallDetection.IsTouchingLayers(LayerMask.GetMask("Player")) ||
            _wallDetection.IsTouchingLayers(LayerMask.GetMask("NPC"))) {

            _movement.FlipHorizontalDirection();
            _movement.FaceMoveDirection();

            _detectionTimer = 0;
        }
    }
}