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
    [SerializeField] private AudioManager _audioManager;
    [SerializeField] private BackgroundMusic _backgroundMusic;
    [Header("Buttons")]
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _resumeButton;
    [SerializeField] private Button _soundButton;
    [SerializeField] private Button _musicButton;
    [SerializeField] private Button _menuButton;
    [SerializeField] private Button _restartButton;

    [Header("Settings")]
    [SerializeField] private float _animationDuration;

    [Header("Sprites")]

    [SerializeField] private Sprite _soundOnSprite;
    [SerializeField] private Sprite _soundOffSprite;
    [SerializeField] private Sprite _musicOnSprite;
    [SerializeField] private Sprite _musicOffSprite;
    private bool _isSoundOn = true;
    private bool _isMusicOn = true;

    private Image _blackBackgroundImage;
    void Awake()
    {
        if (_blackBackgroundObject == null)
        {
            Debug.LogWarning("SettingsUI: _blackBackgroundObject is not assigned in the inspector!");
        }
        else
        {
            _blackBackgroundImage = _blackBackgroundObject.GetComponent<Image>();
        }

        if (_settingsPopUpObject == null)
        {
            Debug.LogWarning("SettingsUI: _settingsPopUpObject is not assigned in the inspector!");
        }
        else
        {
            _settingsPopUpObject.transform.localScale = Vector3.zero;
        }

        if (_settingsButton != null)
            _settingsButton.onClick.AddListener(OnSettingsButtonClicked);
        else
            Debug.LogWarning("SettingsUI: _settingsButton is not assigned in the inspector!");

        if (_resumeButton != null)
            _resumeButton.onClick.AddListener(OnResumeButtonClicked);
        else
            Debug.LogWarning("SettingsUI: _resumeButton is not assigned in the inspector!");

        if (_menuButton != null)
            _menuButton.onClick.AddListener(OnMenuButtonClicked);
        else
            Debug.LogWarning("SettingsUI: _menuButton is not assigned in the inspector!");

        if (_restartButton != null)
            _restartButton.onClick.AddListener(OnRestartButtonClicked);
        else
            Debug.LogWarning("SettingsUI: _restartButton is not assigned in the inspector!");

        if (_soundButton != null)
            _soundButton.onClick.AddListener(OnSoundButtonClicked);
        else
            Debug.LogWarning("SettingsUI: _soundButton is not assigned in the inspector!");

        if (_musicButton != null)
            _musicButton.onClick.AddListener(OnMusicButtonClicked);
        else
            Debug.LogWarning("SettingsUI: _musicButton is not assigned in the inspector!");

        // Load saved preferences
        LoadAudioPreferences();
    }

    private void Start()
    {
        // Apply the loaded preferences to audio systems
        ApplyAudioSettings();
    }

    private void LoadAudioPreferences()
    {
        // Load saved preferences (1 = on, 0 = off, default to 1 if not set)
        _isSoundOn = PlayerPrefs.GetInt("SoundEnabled", 1) == 1;
        _isMusicOn = PlayerPrefs.GetInt("MusicEnabled", 1) == 1;

        // Update button sprites to match loaded state
        if (_soundButton != null && _soundButton.image != null)
        {
            _soundButton.image.sprite = _isSoundOn ? _soundOnSprite : _soundOffSprite;
        }

        if (_musicButton != null && _musicButton.image != null)
        {
            _musicButton.image.sprite = _isMusicOn ? _musicOnSprite : _musicOffSprite;
        }
    }

    private void ApplyAudioSettings()
    {
        // Apply sound effects setting
        if (_audioManager != null)
        {
            _audioManager.SetSoundEffectsMute(!_isSoundOn);
        }
        else if (AudioManager.Instance != null)
        {
            AudioManager.Instance.SetSoundEffectsMute(!_isSoundOn);
        }

        // Apply music setting
        if (_backgroundMusic != null)
        {
            _backgroundMusic.SetMusicMute(!_isMusicOn);
        }
        else if (BackgroundMusic.Instance != null)
        {
            BackgroundMusic.Instance.SetMusicMute(!_isMusicOn);
        }
    }

    void Update()
    {
        if (_winloseui == null)
            return;

        if (Input.GetKeyDown(KeyCode.Escape) && _winloseui._ifWinOrLose == false)
        {
            OnSettingsButtonClicked();
            if (_cursorManager != null)
                _cursorManager.CursorVisible();
        }

    }

    private void OnSettingsButtonClicked()
    {
        if (GameManager.Instance == null)
        {
            Debug.LogWarning("SettingsUI: GameManager.Instance is null!");
            return;
        }

        GameManager.Instance.ChangeGameState(GameState.Pause);

        if (_blackBackgroundObject != null)
            _blackBackgroundObject.SetActive(true);

        if (_settingsPopUpObject != null)
            _settingsPopUpObject.SetActive(true);

        if (_blackBackgroundImage != null)
            _blackBackgroundImage.DOFade(0.8f, _animationDuration).SetEase(Ease.Linear);

        if (_settingsPopUpObject != null)
            _settingsPopUpObject.transform.DOScale(1.5f, _animationDuration).SetEase(Ease.OutBack);

        Invoke(nameof(StopTime), 0.15f);

    }

    private void OnResumeButtonClicked()
    {
        ResumeTime();

        if (_cursorManager != null)
            _cursorManager.CursorUnvisible();

        if (_blackBackgroundImage != null)
            _blackBackgroundImage.DOFade(0f, _animationDuration).SetEase(Ease.Linear);

        if (_settingsPopUpObject != null)
        {
            _settingsPopUpObject.transform.DOScale(0f, _animationDuration).SetEase(Ease.InBack).OnComplete(() =>
            {
                if (GameManager.Instance != null)
                    GameManager.Instance.ChangeGameState(GameState.Resume);

                if (_blackBackgroundObject != null)
                    _blackBackgroundObject.SetActive(false);

                if (_settingsPopUpObject != null)
                    _settingsPopUpObject.SetActive(false);
            });
        }

    }

    private void OnRestartButtonClicked()
    {
        ResumeTime();

        if (_blackBackgroundObject != null)
            _blackBackgroundObject.SetActive(false);

        if (_settingsPopUpObject != null)
            _settingsPopUpObject.SetActive(false);

        SceneManager.LoadScene(Consts.Scenes.GAME_SCENE);

    }

    private void StopTime()
    {
        Time.timeScale = 0f;
    }

    private void ResumeTime()
    {
        Time.timeScale = 1f;
    }

    private void OnSoundButtonClicked()
    {
        // Toggle sound state
        _isSoundOn = !_isSoundOn;

        // Update button sprite
        if (_soundButton != null && _soundButton.image != null)
        {
            _soundButton.image.sprite = _isSoundOn ? _soundOnSprite : _soundOffSprite;
        }

        // Apply to AudioManager
        if (_audioManager != null)
        {
            _audioManager.SetSoundEffectsMute(!_isSoundOn);
        }
        else if (AudioManager.Instance != null)
        {
            AudioManager.Instance.SetSoundEffectsMute(!_isSoundOn);
        }

        // Save preference
        PlayerPrefs.SetInt("SoundEnabled", _isSoundOn ? 1 : 0);
        PlayerPrefs.Save();
    }

    private void OnMusicButtonClicked()
    {
        // Toggle music state
        _isMusicOn = !_isMusicOn;

        // Update button sprite
        if (_musicButton != null && _musicButton.image != null)
        {
            _musicButton.image.sprite = _isMusicOn ? _musicOnSprite : _musicOffSprite;
        }

        // Apply to BackgroundMusic
        if (_backgroundMusic != null)
        {
            _backgroundMusic.SetMusicMute(!_isMusicOn);
        }
        else if (BackgroundMusic.Instance != null)
        {
            BackgroundMusic.Instance.SetMusicMute(!_isMusicOn);
        }

        // Save preference
        PlayerPrefs.SetInt("MusicEnabled", _isMusicOn ? 1 : 0);
        PlayerPrefs.Save();
    }

    private void OnMenuButtonClicked()
    {
        ResumeTime();
        SceneManager.LoadScene(Consts.Scenes.MENU_SCENE);
    }
}
