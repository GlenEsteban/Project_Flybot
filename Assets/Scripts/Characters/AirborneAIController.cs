using UnityEngine;
using UnityEngine.AI;

public class AirborneAIController : MonoBehaviour {
    [Header("Targeting")]
    [SerializeField] private bool _isFollowingTarget = true;
    [SerializeField] private Transform _target;

    [Header("Collision Detection")]
    [SerializeField] private bool _hasWallDetection = true;
    [SerializeField] private Collider2D _wallDetection;
    [SerializeField] private float _detectionRatePerSec = 0.1f;

    private Vector2 _moveDirection;
    private float _detectionTimer;
    private NavMeshAgent _navMeshAgent;
    private Movement _movement;

    private void Awake() {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _movement = GetComponent<Movement>();

        _navMeshAgent.updateRotation = false;
        _navMeshAgent.updateUpAxis = false;
    }


    private void Update() {
        if (_isFollowingTarget) {
            _navMeshAgent.enabled = true;
            _navMeshAgent.speed = _movement.GetMoveSpeed();
            _navMeshAgent.SetDestination(_target.position);

            _movement.SetCanMove(false);
            _moveDirection = (_target.position - this.transform.position).normalized;
            _movement.FaceDirection(_moveDirection);
        }
        else {
            _navMeshAgent.enabled = false;
            _movement.SetCanMove(true);

            _movement.SetMoveDirection(transform.right);

            _detectionTimer += Time.deltaTime;

            if (_hasWallDetection && _detectionTimer > _detectionRatePerSec) {
                HandleWallDetectionBehavior();
            }
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
