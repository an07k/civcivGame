using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;


public class PlayerHealthUI : MonoBehaviour
{
    [Header("Referances")]

    [SerializeField] private Image[] _playerHealthImages;

    [Header("Sprites")]
    [SerializeField] private Sprite _playerHealthySprite;
    [SerializeField] private Sprite _playerUnhealthySprite;

    [Header("Settings")]

    [SerializeField] private float _scaleDuration;
    private RectTransform[] _playerHealthRectTransforms;


    private void Awake()
    {
        if (_playerHealthImages == null || _playerHealthImages.Length == 0)
        {
            Debug.LogWarning("PlayerHealthUI: _playerHealthImages array is not assigned or empty in the inspector!");
            return;
        }

        _playerHealthRectTransforms = new RectTransform[_playerHealthImages.Length];

        for (int i = 0; i < _playerHealthImages.Length; i += 1) {
            if (_playerHealthImages[i] == null)
            {
                Debug.LogWarning($"PlayerHealthUI: _playerHealthImages[{i}] is null!");
                continue;
            }

            _playerHealthRectTransforms[i] = _playerHealthImages[i].gameObject.GetComponent<RectTransform>();

        }

    }

    public void AnimateDamage()
    {
        if (_playerHealthImages == null || _playerHealthRectTransforms == null)
        {
            Debug.LogWarning("PlayerHealthUI: Cannot animate damage, arrays are null!");
            return;
        }

        for (int i = 0; i < _playerHealthImages.Length; i++)
        {
            if (_playerHealthImages[i] == null || _playerHealthRectTransforms[i] == null)
                continue;

            if (_playerHealthImages[i].sprite == _playerHealthySprite)
            {
                AnimateDamageSprite(_playerHealthImages[i], _playerHealthRectTransforms[i]);
                break;
            }
        }
    }

    #if UNITY_EDITOR
    // Debug helper method for testing damage animation on all hearts
    public void AnimateDamage4All()
    {
        if (_playerHealthImages == null || _playerHealthRectTransforms == null)
        {
            Debug.LogWarning("PlayerHealthUI: Cannot animate damage for all, arrays are null!");
            return;
        }

        for (int i = 0; i < _playerHealthImages.Length; i++)
        {
            if (_playerHealthImages[i] == null || _playerHealthRectTransforms[i] == null)
                continue;

            AnimateDamageSprite(_playerHealthImages[i], _playerHealthRectTransforms[i]);
        }

    }
    #endif
    private void AnimateDamageSprite(Image activeImage, RectTransform activeImageTransform)
    {
        if (activeImage == null || activeImageTransform == null)
        {
            Debug.LogWarning("PlayerHealthUI: Cannot animate damage sprite, image or transform is null!");
            return;
        }

        activeImageTransform.DOScale(0f, _scaleDuration).SetEase(Ease.InBack).OnComplete(() =>
        {
            if (activeImage != null)
                activeImage.sprite = _playerUnhealthySprite;
            if (activeImageTransform != null)
                activeImageTransform.DOScale(1f, _scaleDuration).SetEase(Ease.OutBack);
        });

    }
}
