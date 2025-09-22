using UnityEngine;

public class PlayerExpressionChecks : MonoBehaviour {

    [SerializeField] private LightBulbPost _lightBulbPost;
    private SpriteAnimator _playerAnimator;
    private void Awake() {
        _playerAnimator = GetComponent<SpriteAnimator>();

        _lightBulbPost.LightBulbOn += LightBulbOnExpression;
    }

    private void LightBulbOnExpression() {
        _playerAnimator.SetPlayerExpression(Expression.SuperHappy);
    }
}
