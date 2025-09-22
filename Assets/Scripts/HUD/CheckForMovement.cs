using UnityEngine;

public class CheckForMovement : MonoBehaviour {
    [SerializeField] Rigidbody2D _playerRB;
    [SerializeField] Animator _UIAnimator; 

    private void Update() {
        if (_playerRB.velocity.magnitude > 2f) { 
            _UIAnimator.SetBool("IsVisible", false);
        }
    }
}