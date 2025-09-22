using UnityEngine;

public class SpriteAnimator : MonoBehaviour {
    [SerializeField] private Transform _playerSprite;
    [SerializeField] private Animator _clawAnimator;
    [SerializeField] private Animator _displayAnimator;

    private bool _isFacingRight = true;

    public void HandleSpriteFlip(Vector2 direction) {
        if (direction.x == 0) return;

        if (_isFacingRight && direction.x < 0) {
            _playerSprite.Rotate(0, 180, 0);
            _isFacingRight = false;
        }
        else if (!_isFacingRight && direction.x > 0) {
            _playerSprite.Rotate(0, 180, 0);
            _isFacingRight = true;
        }
    }

    public void SetClawAnimation(bool state) {
        _clawAnimator.SetBool("IsUsingClaw", state);
    }

    public void SetPlayerExpression(Expression expression) {
        switch (expression) {
            case Expression.Happy:
                _displayAnimator.SetBool("IsHappy", true);
                break;
            case Expression.Neutral:
                _displayAnimator.SetBool("IsNeutral", true);
                break;
            case Expression.Upset:
                _displayAnimator.SetBool("IsUpset", true);
                break;
            case Expression.SuperHappy:
                _displayAnimator.SetBool("IsSuperHappy", true);
                break;
        }
    }
}

public enum Expression {
    Happy,
    Neutral,
    Upset,
    SuperHappy
}