using System;
using System.Collections;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class PlayerStateUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private RectTransform _playerWalkingTransform;
    [SerializeField] private RectTransform _playerSlidingTransform;

    [SerializeField] private RectTransform _boostSpeedTransform;
    [SerializeField] private RectTransform _boostJumpTransform;
    [SerializeField] private RectTransform _boostSlowTransform;
    [SerializeField] private RectTransform _boostScaleTransform;

    [Header("Images")]

    [SerializeField] private Image _goldWheatImage;
    [SerializeField] private Image _holyWheatImage;
    [SerializeField] private Image _rottenWheatImage;
    [SerializeField] private Image _donkeyWheatImage;

    [Header("Countdown Fill Images")]
    [SerializeField] private Image _boostSpeedCountdownImage;
    [SerializeField] private Image _boostJumpCountdownImage;
    [SerializeField] private Image _boostSlowCountdownImage;
    [SerializeField] private Image _boostScaleCountdownImage;

    [Header("Sprites")]

    [SerializeField] private Sprite _playerWalkingActiveSprite;
    [SerializeField] private Sprite _playerWalkingPassiveSprite;

    [SerializeField] private Sprite _playerSlidingActiveSprite;

    [SerializeField] private Sprite _playerSlidingInactiveSprite;

    [Header("Settings")]

    [SerializeField] private float _moveDuration;
    [SerializeField] private Ease _moveEase;
    private Image playerWalkingImage;
    private Image playerSlidingImage;

    public RectTransform GetBoosterSpeedTransform => _boostSpeedTransform;
    public RectTransform GetBoosterJumpTransform => _boostJumpTransform;
    public RectTransform GetBoosterSlowTransform => _boostSlowTransform;

    public RectTransform GetBoosterScaleTransform => _boostScaleTransform;

    public Image GetGoldWheatImage => _goldWheatImage;
    public Image GetHolyWheatImage => _holyWheatImage;
    public Image GetRottenWheatImage => _rottenWheatImage;

    public Image GetDonkeyWheatImage => _donkeyWheatImage;

    public Image GetBoostSpeedCountdownImage => _boostSpeedCountdownImage;
    public Image GetBoostJumpCountdownImage => _boostJumpCountdownImage;
    public Image GetBoostSlowCountdownImage => _boostSlowCountdownImage;
    public Image GetBoostScaleCountdownImage => _boostScaleCountdownImage;
    void Awake()
    {
        if (_playerWalkingTransform == null)
        {
            Debug.LogWarning("PlayerStateUI: _playerWalkingTransform is not assigned in the inspector!");
        }
        else
        {
            playerWalkingImage = _playerWalkingTransform.GetComponent<Image>();
        }

        if (_playerSlidingTransform == null)
        {
            Debug.LogWarning("PlayerStateUI: _playerSlidingTransform is not assigned in the inspector!");
        }
        else
        {
            playerSlidingImage = _playerSlidingTransform.GetComponent<Image>();
        }

    }

    void Start()
    {
        if (_playerController == null)
        {
            Debug.LogWarning("PlayerStateUI: _playerController is not assigned in the inspector!");
            return;
        }

        _playerController.OnPlayerStateChanged += PlayerController_OnPlayerStateChanged;

        SetStateUserInterface(_playerWalkingActiveSprite, _playerSlidingInactiveSprite, _playerWalkingTransform, _playerSlidingTransform);
    }

    private void PlayerController_OnPlayerStateChanged(PlayerState playerState)
    {
        switch (playerState)
        {
            case PlayerState.Idle:
            case PlayerState.Move:
                //ÜSTTEKİ KART ACILACAK
                SetStateUserInterface(_playerWalkingActiveSprite, _playerSlidingInactiveSprite, _playerWalkingTransform, _playerSlidingTransform);
                break;

            case PlayerState.SlideIdle:
            case PlayerState.Slide:
                //ALTTAKİ KART ACILACAK
                SetStateUserInterface(_playerWalkingPassiveSprite, _playerSlidingActiveSprite, _playerSlidingTransform, _playerWalkingTransform);
                break;
        }
    }

    private void SetStateUserInterface(Sprite playerWalkingSprite, Sprite playerSlidingSprite,
            RectTransform activeTransform, RectTransform passiveTransform)
    {
        if (playerWalkingImage == null || playerSlidingImage == null)
        {
            Debug.LogWarning("PlayerStateUI: Player images are null!");
            return;
        }

        if (activeTransform == null || passiveTransform == null)
        {
            Debug.LogWarning("PlayerStateUI: Transforms are null!");
            return;
        }

        playerWalkingImage.sprite = playerWalkingSprite;
        playerSlidingImage.sprite = playerSlidingSprite;

        activeTransform.DOAnchorPosX(-25f, _moveDuration).SetEase(_moveEase);
        passiveTransform.DOAnchorPosX(-90f, _moveDuration).SetEase(_moveEase);
    }

    private IEnumerator SetBoosterUserInterface(RectTransform activeTransform, Image boosterImage, Image wheatImage,
            Sprite activeSprite, Sprite passiveSprite, Sprite activeWheatSprite, Sprite passiveWheatSprite, float duration, Image countdownImage)
    {
        if (activeTransform == null || boosterImage == null || wheatImage == null)
        {
            Debug.LogWarning("PlayerStateUI: Cannot animate booster UI, required references are null!");
            yield break;
        }

        boosterImage.sprite = activeSprite;
        wheatImage.sprite = activeWheatSprite;
        activeTransform.DOAnchorPosX(25f, _moveDuration).SetEase(_moveEase);

        // Initialize countdown image
        if (countdownImage != null)
        {
            countdownImage.fillAmount = 1f;
            countdownImage.enabled = true;
        }

        // Countdown animation
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            if (countdownImage != null)
            {
                countdownImage.fillAmount = 1f - (elapsedTime / duration);
            }
            yield return null;
        }

        // Reset countdown image
        if (countdownImage != null)
        {
            countdownImage.fillAmount = 0f;
            countdownImage.enabled = false;
        }

        boosterImage.sprite = passiveSprite;
        wheatImage.sprite = passiveWheatSprite;
        activeTransform.DOAnchorPosX(90f, _moveDuration).SetEase(_moveEase);
    }

    public void BoosterUIAnimations(RectTransform activeTransform, Image boosterImage, Image wheatImage,
            Sprite activeSprite, Sprite passiveSprite, Sprite activeWheatSprite, Sprite passiveWheatSprite, float duration, Image countdownImage)
            {
        StartCoroutine(SetBoosterUserInterface(activeTransform, boosterImage, wheatImage, activeSprite,
        passiveSprite, activeWheatSprite, passiveWheatSprite, duration, countdownImage));
            }
}
