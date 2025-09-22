using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class GrabbableItem : MonoBehaviour {
    // Events
    public event Action ItemBroke;
    public event Action OnGrabbed;
    public event Action OnDropped;

    // Item Attributes and References
    [Header("General")]
    [SerializeField] private string _name;
    [SerializeField] private bool _canBeGrabbed = true;
    [SerializeField] private Collider2D _collider;

    [Header("Movement")]

    [Header("Fragility")]
    [SerializeField] private bool _isFragile;
    [SerializeField] private float _speedTillShatter = 3f;
    [SerializeField] private ParticleSystem _shatterParticles;

    // Cached References
    private Rigidbody2D _rb;

    // Fragility Checks Variables
    private bool _canShatter;
    private bool _isBroken;

    // Accessor Methods
    public string GetName() {
        return _name;
    }
    public bool GetIsBroken() {
        return _isBroken;
    }
    public void SetIsFragile(bool state) {
        _isFragile = state;
    }
    public bool GetCanBeGrabbed() {
        return _canBeGrabbed;
    }
    public void SetCanBeGrabbed(bool state) {
        _canBeGrabbed = state;
    }

    private void Awake() {
        _rb = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
    }

    private void Update() {
        if (_isBroken) return;

        if (_isFragile && _canShatter && 
            (_collider.IsTouchingLayers(LayerMask.GetMask("Ground")) ||
            _collider.IsTouchingLayers(LayerMask.GetMask("NPC")) ||
            _collider.IsTouchingLayers(LayerMask.GetMask("Platform")))) {
            _isBroken = true;
            _canBeGrabbed = false;

            ItemBroke?.Invoke();
                        
            _shatterParticles.Play();
        } 

        _canShatter = _rb.velocity.magnitude > _speedTillShatter;
    }
}