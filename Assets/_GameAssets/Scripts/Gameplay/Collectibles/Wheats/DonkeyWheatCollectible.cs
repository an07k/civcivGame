
using UnityEngine;

public class DonkeyWheatCollectible : MonoBehaviour, ICollectible
{
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private Transform _transform;
    public void Collect()
    {
        _playerController.IncreaseScale(1, 3);
        Destroy(gameObject);
    }
}
