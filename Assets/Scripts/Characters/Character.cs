using UnityEngine;

public class Character : MonoBehaviour {
    private void Start() {
        CharacterManager.Instance.Add(this);
    }

    private void OnDisable() {
        CharacterManager.Instance.Remove(this);
    }
}
