using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Movement : MonoBehaviour {
    // Movement Variables
    [Range(1f, 100f), SerializeField] float _movementSpeed;
    public Vector2 _moveDirection;

    // References
    private Rigidbody2D _rb;
    private NavMeshAgent _navMeshAgent;

    // Mobility Checks Variables
    private bool _canMove = true;

    // Accessor Methods
    public void SetCanMove(bool state) {
        _canMove = state;
    }

    public float GetMoveSpeed() {
        return _movementSpeed;
    }

    public void SetMoveDirection(Vector2 direction) {
        _moveDirection = direction.normalized;
    }

    private void Awake() {
        _rb = GetComponent<Rigidbody2D>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Start() {
        if (_navMeshAgent != null) {
            _navMeshAgent.speed = _movementSpeed;
        }
    }

    void LateUpdate() {
        if (!_canMove) { return; }

        _rb.velocity += _movementSpeed * _moveDirection * Time.deltaTime;
    }

    public void HardStopMovement() {
        _rb.velocity = Vector3.zero;
    }

    public void FlipHorizontalDirection() {
        _moveDirection = -_moveDirection;
    }

    public void FaceMoveDirection() {
        if (Vector2.Dot(_moveDirection, Vector2.right) > 0) {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

    public void FaceDirection(Vector2 direction) {
        if (Vector2.Dot(direction, Vector2.right) > 0) {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

}