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
        _blackBackgroundImage = _blackBackgroundObject.GetComponent<Image>();
        _winPopUpRect = _winPopUp.GetComponent<RectTransform>();
        _losePopUpRect = _losePopUp.GetComponent<RectTransform>();

        _loseTryAgainButton.onClick.AddListener(OnTryAgainButtonClicked);
        _winRestartButton.onClick.AddListener(OnTryAgainButtonClicked);

        _loseMenuButton.onClick.AddListener(OnMenuButtonClicked);
        _winMenuButton.onClick.AddListener(OnMenuButtonClicked);
    }

    public void OnGameWin()
    {
        _ifWinOrLose = true;
        _cursorManager.CursorVisible();
        _blackBackgroundObject.SetActive(true);
        _winPopUp.SetActive(true);

        _blackBackgroundImage.DOFade(0.8f, _animationDuration).SetEase(Ease.Linear);
        _winPopUpRect.DOScale(1.5f, _animationDuration).SetEase(Ease.OutBack);
        
    }

    public void OnGameLose()
    {
        _ifWinOrLose = true;
        _cursorManager.CursorVisible();
        _blackBackgroundObject.SetActive(true);
        _losePopUp.SetActive(true);

        _blackBackgroundImage.DOFade(0.8f, _animationDuration).SetEase(Ease.Linear);
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
