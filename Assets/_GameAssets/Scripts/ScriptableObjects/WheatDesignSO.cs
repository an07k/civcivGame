using UnityEngine;

[CreateAssetMenu(fileName = "WheatDesignSO", menuName = "ScriptableObjects/WheatDesignSO")]

public class WheatDesignSO : ScriptableObject
{
    [SerializeField] private float _increaseDecreaseMultiplier;
    [SerializeField] private float _resetBoostDuration;
    [SerializeField] private Sprite activeSprite;
    [SerializeField] private Sprite passiveSprite;
    [SerializeField] private Sprite activeWheatSprite;
    [SerializeField] private Sprite passiveWheatSprite;

    public float IncreaseDecreaseMultiplier => _increaseDecreaseMultiplier;
    public float ResetBoostDuration => _resetBoostDuration;

    public Sprite ActiveSprite => activeSprite;
    public Sprite PassiveSprite => passiveSprite;
    public Sprite ActiveWheatSprite => activeWheatSprite;
    public Sprite PassiveWheatSprite => passiveWheatSprite;
}
