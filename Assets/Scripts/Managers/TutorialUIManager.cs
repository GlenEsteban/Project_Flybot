using UnityEngine;

public class TutorialUIManager : MonoBehaviour {
    [SerializeField] Animator _moveTutorialUIAnimator;
    [SerializeField] Animator _grabTutorialUIAnimator;
    [SerializeField] Animator _lightpostTutorialUIAnimator;

    public void DisplayMoveTutorialUI() {
        _grabTutorialUIAnimator.SetBool("IsVisible", true);
    }

    public void DontDisplayMoveTutorialUI() {
        _grabTutorialUIAnimator.SetBool("IsVisible", false);
    }

    public void DisplayGrabTutorialUI() {
        _grabTutorialUIAnimator.SetBool("IsVisible", true);
    }

    public void DontDisplayGrabTutorialUI() {
        _grabTutorialUIAnimator.SetBool("IsVisible", false);
    }
    public void DisplayLightpostTutorialUI() {
        _lightpostTutorialUIAnimator.SetBool("IsVisible", true);
    }

    public void DontDisplayLightpostTutorialUI() {
        _lightpostTutorialUIAnimator.SetBool("IsVisible", false);
    }
}
