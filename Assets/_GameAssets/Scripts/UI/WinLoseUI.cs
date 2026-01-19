using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinLoseUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject _blackBackgroundObject;
    [SerializeField] private CursorManager _cursorManager;
    private Image _blackBackgroundImage;
    private RectTransform _winPopUpRect;
    private RectTransform _losePopUpRect;
    public bool _ifWinOrLose;

    [SerializeField] private GameObject _winPopUp;
    [SerializeField] private GameObject _losePopUp;
    [Header("Buttons")]

    [SerializeField] private Button _loseTryAgainButton;
    [SerializeField] private Button _loseMenuButton;
    [SerializeField] private Button _winRestartButton;
    [SerializeField] private Button _winMenuButton;

    [Header("Settings")]

    [SerializeField] private float _animationDuration = 0.3f;

    void Awake()
    {
        if (_blackBackgroundObject == null)
        {
            Debug.LogWarning("WinLoseUI: _blackBackgroundObject is not assigned in the inspector!");
        }
        else
        {
            _blackBackgroundImage = _blackBackgroundObject.GetComponent<Image>();
        }

        if (_winPopUp == null)
        {
            Debug.LogWarning("WinLoseUI: _winPopUp is not assigned in the inspector!");
        }
        else
        {
            _winPopUpRect = _winPopUp.GetComponent<RectTransform>();
        }

        if (_losePopUp == null)
        {
            Debug.LogWarning("WinLoseUI: _losePopUp is not assigned in the inspector!");
        }
        else
        {
            _losePopUpRect = _losePopUp.GetComponent<RectTransform>();
        }

        if (_loseTryAgainButton != null)
            _loseTryAgainButton.onClick.AddListener(OnTryAgainButtonClicked);
        else
            Debug.LogWarning("WinLoseUI: _loseTryAgainButton is not assigned in the inspector!");

        if (_winRestartButton != null)
            _winRestartButton.onClick.AddListener(OnTryAgainButtonClicked);
        else
            Debug.LogWarning("WinLoseUI: _winRestartButton is not assigned in the inspector!");

        if (_loseMenuButton != null)
            _loseMenuButton.onClick.AddListener(OnMenuButtonClicked);
        else
            Debug.LogWarning("WinLoseUI: _loseMenuButton is not assigned in the inspector!");

        if (_winMenuButton != null)
            _winMenuButton.onClick.AddListener(OnMenuButtonClicked);
        else
            Debug.LogWarning("WinLoseUI: _winMenuButton is not assigned in the inspector!");
    }

    public void OnGameWin()
    {
        _ifWinOrLose = true;

        if (_cursorManager != null)
            _cursorManager.CursorVisible();
        else
            Debug.LogWarning("WinLoseUI: _cursorManager is null!");

        if (_blackBackgroundObject != null)
            _blackBackgroundObject.SetActive(true);
        else
            Debug.LogWarning("WinLoseUI: Cannot show win popup, _blackBackgroundObject is null!");

        if (_winPopUp != null)
            _winPopUp.SetActive(true);
        else
            Debug.LogWarning("WinLoseUI: Cannot show win popup, _winPopUp is null!");

        if (_blackBackgroundImage != null)
            _blackBackgroundImage.DOFade(0.8f, _animationDuration).SetEase(Ease.Linear);

        if (_winPopUpRect != null)
            _winPopUpRect.DOScale(1.5f, _animationDuration).SetEase(Ease.OutBack);

    }

    public void OnGameLose()
    {
        _ifWinOrLose = true;

        if (_cursorManager != null)
            _cursorManager.CursorVisible();
        else
            Debug.LogWarning("WinLoseUI: _cursorManager is null!");

        if (_blackBackgroundObject != null)
            _blackBackgroundObject.SetActive(true);
        else
            Debug.LogWarning("WinLoseUI: Cannot show lose popup, _blackBackgroundObject is null!");

        if (_losePopUp != null)
            _losePopUp.SetActive(true);
        else
            Debug.LogWarning("WinLoseUI: Cannot show lose popup, _losePopUp is null!");

        if (_blackBackgroundImage != null)
            _blackBackgroundImage.DOFade(0.8f, _animationDuration).SetEase(Ease.Linear);

        if (_losePopUpRect != null)
            _losePopUpRect.DOScale(1.5f, _animationDuration).SetEase(Ease.OutBack);
    }

    private void OnTryAgainButtonClicked()
    {
        SceneManager.LoadScene(Consts.Scenes.GAME_SCENE);
    }

    private void OnMenuButtonClicked()
    {
        SceneManager.LoadScene(Consts.Scenes.MENU_SCENE);
    }
}
