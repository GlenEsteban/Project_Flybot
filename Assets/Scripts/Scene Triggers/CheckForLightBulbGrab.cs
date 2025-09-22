using UnityEngine;

public class CheckForLightBulbGrab : MonoBehaviour {
    [SerializeField] private Animator _UIAnimator;

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.GetComponent<PlayerController>() == null) return;

        GrabbableItem item = collision.gameObject.GetComponent<GrabbableItem>();

        if (item != null && item.GetName() == "Lightbulb") {
            _UIAnimator.SetBool("IsVisible", false);
        }
    }
}