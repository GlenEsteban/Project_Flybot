using System;
using Unity.VisualScripting;
using UnityEngine;

public class LightBulbPost : MonoBehaviour {
    public event Action LightBulbOn;
    [SerializeField] private GrabAbility _player;
    [SerializeField] Transform _lightbulbHoldPoint;
    private LightBulbAnimator _lightBulbAnimator;
    private Rigidbody2D _lightBulbRB;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.GetComponent<LightBulbAnimator>() != null && collision.GetComponent<GrabbableItem>().GetIsBroken() != true) {
            Transform collisionGameObject = collision.gameObject.transform;
            collisionGameObject.SetParent(null);

            _lightBulbAnimator = collision.GetComponent<LightBulbAnimator>();
            _lightBulbAnimator.AnimateStateLight();

            _player.Drop();

            _lightBulbRB = collision.GetComponent<Rigidbody2D>();

            _lightBulbRB.bodyType = RigidbodyType2D.Kinematic;
            _lightBulbRB.velocity = Vector3.zero;
            collision.transform.position = _lightbulbHoldPoint.transform.position;
            collision.transform.rotation = Quaternion.identity;

            LightBulbOn?.Invoke();

            GameStateManager.Instance.ChangeGameState(GameState.GameWin);
        }
    }
}