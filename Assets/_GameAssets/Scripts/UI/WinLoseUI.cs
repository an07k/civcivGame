using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class WinLoseUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject _blackBackgroundObject;
    private Image _blackBackgroundImage;
    private RectTransform _winPopUpRect;
    private RectTransform _losePopUpRect;

    [SerializeField] private GameObject _winPopUp;
    [SerializeField] private GameObject _losePopUp;

    [Header("Settings")]

    [SerializeField] private float _animationDuration = 0.3f;

    void Awake()
    {
        _blackBackgroundImage = _blackBackgroundObject.GetComponent<Image>();
        _winPopUpRect = _winPopUp.GetComponent<RectTransform>();
        _losePopUpRect = _losePopUp.GetComponent<RectTransform>();
    }

    public void OnGameWin()
    {
        _blackBackgroundObject.SetActive(true);
        _winPopUp.SetActive(true);

        _blackBackgroundImage.DOFade(0.8f, _animationDuration).SetEase(Ease.Linear);
        _winPopUpRect.DOScale(1.5f, _animationDuration).SetEase(Ease.OutBack);
    }

    public void OnGameLose()
    {
        _blackBackgroundObject.SetActive(true);
        _losePopUp.SetActive(true);

        _blackBackgroundImage.DOFade(0.8f, _animationDuration).SetEase(Ease.Linear);
        _losePopUpRect.DOScale(1.5f, _animationDuration).SetEase(Ease.OutBack);
    }

    public void OnRestart()
    {
        _blackBackgroundImage.DOFade(0f, _animationDuration).SetEase(Ease.Linear);

    }
}
