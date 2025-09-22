using UnityEngine;

public class UIAnimator : MonoBehaviour {
    [SerializeField] Animator _UIAnimator;
    
    public void TriggerAnimation(string animationName) {
        _UIAnimator.SetTrigger(animationName);
    }
}