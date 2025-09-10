using UnityEngine;

public class GameManager : MonoBehaviour
{

    [Header("References")]

    [SerializeField] private EggCounterUI _eggCounterUI;
    [Header("Settings")]
    public static GameManager Instance { get; private set; }
    [SerializeField] private int _maxEggCount = 5;

    private int _currentEggCount;
    void Awake()
    {
        Instance = this;
    }

    public void OnEggCollected()
    {
        _currentEggCount += 1;

        _eggCounterUI.SetEggCounterText(_currentEggCount, _maxEggCount);

        if (_currentEggCount == _maxEggCount)
        {
            Debug.Log("Game Over. You WIN!");
            _eggCounterUI.SetEggCompleted();
        }
    }

}
