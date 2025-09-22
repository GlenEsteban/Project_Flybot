using System.Collections.Generic;
using UnityEngine;

public class ItemsManager : MonoBehaviour {
    // Instances
    public static ItemsManager Instance { get; private set; }

    // Item variables
    private List<GrabbableItem> _pickupItems = new List<GrabbableItem>();

    // Pickup Items List Public Methods
    public void Add(GrabbableItem item) {
        _pickupItems.Add(item);
    }
    public void Remove(GrabbableItem item) {
        _pickupItems.Remove(item);
    }
    public bool Contains(GrabbableItem item) {
        return _pickupItems.Contains(item);
    }

    private void Awake() {
        if (Instance != null &&  Instance != this) {
            Destroy(this);
        }
        else {
            Instance = this;
        }
    }
}