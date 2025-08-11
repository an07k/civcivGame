using UnityEngine;


public class GoldWheatCollectible : MonoBehaviour
{
    [SerializeField] private PlayerController _playerController;

    [SerializeField] private float _BoostSpeed;

    [SerializeField] private float _BoostDuration;

    public void Collect()
    {
        _playerController.SetMovementSpeed(_BoostSpeed, _BoostDuration);
        Destroy(gameObject);
    }
}
