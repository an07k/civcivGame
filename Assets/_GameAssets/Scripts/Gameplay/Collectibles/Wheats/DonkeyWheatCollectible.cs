
using UnityEngine;

public class DonkeyWheatCollectible : MonoBehaviour, ICollectible
{
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private Transform _transform;

    [SerializeField] private WheatDesignSO _wheatDesignSO;
    public void Collect()
    {
        _playerController.IncreaseScale(_wheatDesignSO.IncreaseDecreaseMultiplier, _wheatDesignSO.ResetBoostDuration);
        Destroy(gameObject);
    }
}
