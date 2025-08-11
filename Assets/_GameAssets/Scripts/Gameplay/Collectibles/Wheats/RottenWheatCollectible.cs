using UnityEngine;

public class RottenWheatCollectible : MonoBehaviour
{
    [SerializeField] private PlayerController _playerController;

    [SerializeField] private float _decreaseSpeed;

    [SerializeField] private float _decreaseDuration;

    public void Collect()
    {
        _playerController.SetMovementSpeed(_decreaseSpeed, _decreaseDuration);
        Destroy(gameObject);
    }
}
