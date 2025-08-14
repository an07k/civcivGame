using UnityEngine;

public class HolyWheatCollectible : MonoBehaviour, ICollectible
{
    [SerializeField] private PlayerController _playerController;

    [SerializeField] private float _boostJump;

    [SerializeField] private float _jumpBoostDuration;

    public void Collect()
    {   
        _playerController.SetJumpForce(_boostJump, _jumpBoostDuration);
        Destroy(gameObject);
    }
}
