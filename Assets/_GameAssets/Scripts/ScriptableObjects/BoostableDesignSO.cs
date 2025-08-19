using UnityEngine;

[CreateAssetMenu(fileName = "BoostableDesignSO", menuName = "ScriptableObjects/BoostableDesignSO")]

public class BoostableDesignSO : ScriptableObject
{
    [SerializeField] private float _jumpForce;

    public float JumpForce => _jumpForce;
}
