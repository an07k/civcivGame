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
    void Awake()
    {
        playerWalkingImage = _playerWalkingTransform.GetComponent<Image>();
        playerSlidingImage = _playerSlidingTransform.GetComponent<Image>();


    }

    void Start()
    {
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

        playerWalkingImage.sprite = playerWalkingSprite;
        playerSlidingImage.sprite = playerSlidingSprite;

        activeTransform.DOAnchorPosX(-25f, _moveDuration).SetEase(_moveEase);
        passiveTransform.DOAnchorPosX(-90f, _moveDuration).SetEase(_moveEase);
    }

    private IEnumerator SetBoosterUserInterface(RectTransform activeTransform, Image boosterImage, Image wheatImage,
            Sprite activeSprite, Sprite passiveSprite, Sprite activeWheatSprite, Sprite passiveWheatSprite, float duration)
    {
        boosterImage.sprite = activeSprite;
        wheatImage.sprite = activeWheatSprite;
        activeTransform.DOAnchorPosX(25f, _moveDuration).SetEase(_moveEase);

        yield return new WaitForSeconds(duration);

        boosterImage.sprite = passiveSprite;
        wheatImage.sprite = passiveWheatSprite;
        activeTransform.DOAnchorPosX(90f, _moveDuration).SetEase(_moveEase);
    }

    public void BoosterUIAnimations(RectTransform activeTransform, Image boosterImage, Image wheatImage,
            Sprite activeSprite, Sprite passiveSprite, Sprite activeWheatSprite, Sprite passiveWheatSprite, float duration)
            {
        StartCoroutine(SetBoosterUserInterface(activeTransform, boosterImage, wheatImage, activeSprite,
        passiveSprite, activeWheatSprite, passiveWheatSprite, duration));
            }
}
