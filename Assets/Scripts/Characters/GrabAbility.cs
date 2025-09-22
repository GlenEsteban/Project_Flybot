using System;
using System.Collections.Generic;
using UnityEngine;

public class GrabAbility : MonoBehaviour {
    // Events
    public event Action AccidentalDrop;

    // References
    [SerializeField] private Collider2D _playerCollider;
    [SerializeField] private Collider2D _pickupColliderToIgnore;
    public List<GrabbableItem> _itemsInPickupCollider = new List<GrabbableItem>();
    public Transform _targetPickupItem;
    private Rigidbody2D _targetPickupItemRB;
    private float _targetPickupItemRBGravityScale;
    private ClawAttachmentSFX _player;

    // Pickup Action Variables
    [SerializeField] private Vector3 _pickupOffset = Vector3.down;
    [SerializeField] private float _accidentalDropDistance = 1f;


    private void Awake() {
        _player = GetComponent<ClawAttachmentSFX>();

        SetIgnoreCollisionState(true);
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        GrabbableItem item = collision.GetComponent<GrabbableItem>();
        if (item == null) return;
        else {
            _itemsInPickupCollider.Add(item);
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        GrabbableItem item = collision.GetComponent<GrabbableItem>();
        if (item == null) return;
        else {
            _itemsInPickupCollider.Remove(item);
        }
    }

    void LateUpdate() {
        HandleItemGrabMechanics();
    }

    private void HandleItemGrabMechanics() {
        if (_targetPickupItem != null){
            _targetPickupItemRB.MovePosition(this.transform.position + _pickupOffset);
            _targetPickupItemRB.MoveRotation(this.transform.rotation);

            _targetPickupItemRB.velocity = GetComponentInParent<Rigidbody2D>().velocity;

            var distanceFromTargetItem = (_targetPickupItem.position - (this.transform.position + _pickupOffset)).magnitude;

            if (distanceFromTargetItem > _accidentalDropDistance) {
                Drop();
            }
        }
    }

    public void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Hazards")) {
            AccidentalDrop?.Invoke();
        }
    }

    public void Grab() {
        _player.PlayClawClackClosed();

        if (_targetPickupItem == null && _itemsInPickupCollider.Count > 0) {

            _targetPickupItem = _itemsInPickupCollider[0].transform;
            
            if (_targetPickupItem.GetComponent<GrabbableItem>().GetCanBeGrabbed()) {
                _targetPickupItemRB = _targetPickupItem.GetComponent<Rigidbody2D>();
                _targetPickupItemRB.bodyType = RigidbodyType2D.Dynamic;
                _targetPickupItemRBGravityScale = _targetPickupItemRB.gravityScale;
                _targetPickupItemRB.gravityScale = 0;
            }
        }
    }

    public void Drop() {
        _player.PlayClawClackOpen();

        if (_targetPickupItem != null) {
            _targetPickupItemRB.gravityScale = _targetPickupItemRBGravityScale;

            _targetPickupItem = null;
        }
    }

    private void SetIgnoreCollisionState(bool state) {
        int layerA = LayerMask.NameToLayer("IgnoredColliders");
        int layerB = LayerMask.NameToLayer("Item");
        int layerC = LayerMask.NameToLayer("NPC");
        int layerD = LayerMask.NameToLayer("Platform");

        Physics2D.IgnoreLayerCollision(layerA, layerB, state);
        Physics2D.IgnoreLayerCollision(layerA, layerC, state);
        Physics2D.IgnoreLayerCollision(layerA, layerD, state);

    }
}