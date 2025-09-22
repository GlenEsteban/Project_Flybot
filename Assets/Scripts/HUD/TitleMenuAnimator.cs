using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleMenuAnimator : MonoBehaviour {
    [SerializeField] private Animator _TitleMenuAnimator;
    [SerializeField] private SceneLoader _sceneLoader;
    [SerializeField] private Animator _playerAnimator;

    private bool _canPresAnyKey = false;
    private bool _canGameStart = false;

    // Update is called once per frame
    void Update()
    {
        if (!_canPresAnyKey && Input.anyKey) {
            StartCoroutine(DelayPressAnyKey());
        }
        if (_canGameStart && Input.anyKey) {
            StartCoroutine(DelayGameStart());
        }
    }
    IEnumerator DelayPressAnyKey() {
        _TitleMenuAnimator.SetTrigger("PressAnyKey");
        yield return new WaitForSeconds(2f);
        _canGameStart = true;
    }

    IEnumerator DelayGameStart() {
        _TitleMenuAnimator.SetTrigger("GameStart");
        yield return new WaitForSeconds(0.5f);
        _playerAnimator.SetTrigger("GameStart");
        StartCoroutine(_sceneLoader.LoadNextScene(0f));
    }
}