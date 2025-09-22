using UnityEngine;

public class LightBulbAnimator : MonoBehaviour {
    // References
    private Animator _animator;
    private GrabbableItem _item;


    private void Awake() {
        _animator = GetComponent<Animator>();
        _item = GetComponent<GrabbableItem>();

        _item.ItemBroke += AnimateStateBroken;
    }

    public void AnimateStateBroken() {
        _animator.SetBool("IsBroken", true);
    }

    public void AnimateStateLight() {
        _animator.SetBool("IsLightOn", true);
    }
}