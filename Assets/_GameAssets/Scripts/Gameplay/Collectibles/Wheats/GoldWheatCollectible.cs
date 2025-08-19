using UnityEngine;
using UnityEngine.UI;


public class GoldWheatCollectible : MonoBehaviour, ICollectible
{
    [SerializeField] private PlayerStateUI _playerStateUI;
    [SerializeField] private WheatDesignSO _wheatDesignSO;
    [SerializeField] private PlayerController _playerController;

    private RectTransform _playerBoosterTransform;

    private Image _playerBoosterImage;

    private void Awake()
    {
        _playerBoosterTransform = _playerStateUI.GetBoosterSpeedTransform;
        _playerBoosterImage = _playerBoosterTransform.GetComponent<Image>();
    }

    public void Collect()
    {
        _playerController.SetMovementSpeed(_wheatDesignSO.IncreaseDecreaseMultiplier, _wheatDesignSO.ResetBoostDuration);

        _playerStateUI.BoosterUIAnimations(_playerBoosterTransform, _playerBoosterImage,
            _playerStateUI.GetGoldWheatImage, _wheatDesignSO.ActiveSprite, _wheatDesignSO.PassiveSprite,
            _wheatDesignSO.ActiveWheatSprite, _wheatDesignSO.PassiveWheatSprite, _wheatDesignSO.ResetBoostDuration);

        Destroy(gameObject);
    }
}
