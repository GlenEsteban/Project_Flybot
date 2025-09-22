using System.Collections;
using UnityEngine;

public class GameStateManager : MonoBehaviour {
    public static GameStateManager Instance { get; private set; }

    [SerializeField] private BGMPlayer _bgmPlayer;
    [SerializeField] private GrabbableItem _lightBulb;
    [SerializeField] private Animator _playerAnimator;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private SceneLoader _sceneLoader;
    [SerializeField] private GameObject _levelIntro;
    [SerializeField] private GameObject _reloadLevel;

    private static bool _hasStartedLevel = false;

    private GameState _currentGameState;

    private void Awake() {
        if (Instance != null && Instance != this) { 
            Destroy(gameObject);
        }
        else {
            Instance = this;
        }

        _lightBulb.ItemBroke += RunGameOver;
    }

    private void Start() {
        RunGameStart();
    }

    // TEMP: INPUT FOR TESTING GAME STATE CHANGES
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            ChangeGameState(GameState.GameStart);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2)) {
            ChangeGameState(GameState.GameOver);
        }
    }

    public void ChangeGameState(GameState state) {
        if (_currentGameState == state) { return; }

        _currentGameState = state;

        switch (state) {
            case GameState.GameStart:
                RunGameStart();
                break;
            case GameState.GameWin:
                RunGameWin();
                break;
            case GameState.GameOver:
                RunGameOver();
                break;
        }
    }

    private void RunGameStart() {
        if (!_hasStartedLevel) {
            StartCoroutine(LevelIntro());

            _hasStartedLevel = true;
        }
        else {
            _levelIntro.SetActive(false);
            _reloadLevel.SetActive(true);

            StartCoroutine(LevelReload());
        }
    }

    private void RunGameWin() {
        _bgmPlayer.PlayGameWinBGM();

        _hasStartedLevel = false;

        StartCoroutine(_sceneLoader.LoadNextScene(3f));
    }
    private void RunGameOver() {
        _bgmPlayer.StopMusic();
        _playerAnimator.SetTrigger("IsNeutral");
        StartCoroutine(_sceneLoader.ReloadScene(3f));        
    }
    public IEnumerator LevelIntro() {
        _levelIntro.SetActive(true);
        _reloadLevel.SetActive(false);

        _playerController.enabled = false;

        yield return new WaitForSeconds(4f);

        _bgmPlayer.PlayMainBGM();

        _playerController.enabled = true;
    }
    public IEnumerator LevelReload() {
        _levelIntro.SetActive(false);
        _reloadLevel.SetActive(true);

        _playerController.enabled = false;

        yield return new WaitForSeconds(1f);

        _bgmPlayer.PlayMainBGM();

        _playerController.enabled = true;
    }
}

public enum GameState {
    GameStart,
    GameWin,
    GameOver,
}