
using UnityEngine;
using UnityEngine.UI;

public class DonkeyWheatCollectible : MonoBehaviour, ICollectible
{
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private WheatDesignSO _wheatDesignSO;
    [SerializeField] private PlayerStateUI _playerStateUI;

    private RectTransform _playerBoosterTransform;

    private Image _playerBoosterImage;

    private void Awake()
    {
        _playerBoosterTransform = _playerStateUI.GetBoosterScaleTransform;
        _playerBoosterImage = _playerBoosterTransform.GetComponent<Image>();
    }
    public void Collect()
    {
        _playerController.IncreaseScale(_wheatDesignSO.IncreaseDecreaseMultiplier, _wheatDesignSO.ResetBoostDuration);
        
        _playerStateUI.BoosterUIAnimations(_playerBoosterTransform, _playerBoosterImage,
            _playerStateUI.GetDonkeyWheatImage, _wheatDesignSO.ActiveSprite, _wheatDesignSO.PassiveSprite,
            _wheatDesignSO.ActiveWheatSprite, _wheatDesignSO.PassiveWheatSprite, _wheatDesignSO.ResetBoostDuration);
        Destroy(this.gameObject);
    }
}
