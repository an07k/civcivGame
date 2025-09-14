using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private WinLoseUI _winloseui;
    [SerializeField] private GameObject _settingsGameObject;
    [SerializeField] private GameObject _blackBackgroundObject;
    [SerializeField] private GameObject _settingsPopUpObject;
    [SerializeField] private CursorManager _cursorManager;
    [SerializeField] private GameObject _freeCamera;
    [Header("Buttons")]
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _resumeButton;
    [SerializeField] private Button _soundButton;
    [SerializeField] private Button _musicButton;
    [SerializeField] private Button _menuButton;
    [SerializeField] private Button _restartButton;

    [Header("Settings")]
    [SerializeField] private float _animationDuration;

    private Image _blackBackgroundImage;
    void Awake()
    {
        _blackBackgroundImage = _blackBackgroundObject.GetComponent<Image>();
        _settingsPopUpObject.transform.localScale = Vector3.zero;

        _settingsButton.onClick.AddListener(OnSettingsButtonClicked);
        _resumeButton.onClick.AddListener(OnResumeButtonClicked);
        _restartButton.onClick.AddListener(OnRestartButtonClicked);

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && _winloseui._ifWinOrLose == false)
        {
            OnSettingsButtonClicked();
            _cursorManager.CursorVisible();
        }

    }

    private void OnSettingsButtonClicked()
    {
        GameManager.Instance.ChangeGameState(GameState.Pause);

        _blackBackgroundObject.SetActive(true);
        _settingsPopUpObject.SetActive(true);

        _blackBackgroundImage.DOFade(0.8f, _animationDuration).SetEase(Ease.Linear);
        _settingsPopUpObject.transform.DOScale(1.5f, _animationDuration).SetEase(Ease.OutBack);
        Invoke(nameof(StopTime), 0.15f);

    }

    private void OnResumeButtonClicked()
    {
        ResumeTime();
        _cursorManager.CursorUnvisible();

        _blackBackgroundImage.DOFade(0f, _animationDuration).SetEase(Ease.Linear);
        _settingsPopUpObject.transform.DOScale(0f, _animationDuration).SetEase(Ease.InBack).OnComplete(() =>
        {
            GameManager.Instance.ChangeGameState(GameState.Resume);
            _blackBackgroundObject.SetActive(false);
            _settingsPopUpObject.SetActive(false);
        }

        );

    }

    private void OnRestartButtonClicked()
    {
        ResumeTime();
        _blackBackgroundObject.SetActive(false);
        _settingsPopUpObject.SetActive(false);
        SceneManager.LoadScene(Consts.Scenes.GAME_SCENE);
        


    }

    private void SetCameraPassive()
    {
        _freeCamera.SetActive(false);
    }

    private void SetCameraActive()
    {
        _freeCamera.SetActive(true);
    }

    private void StopTime() {
        Time.timeScale = 0f;
    }

    private void ResumeTime() {
        Time.timeScale = 1f;
    }
}
