using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public event Action<GameState> OnGameStateChanged;

    [Header("References")]

    [SerializeField] private SettingsUI _settingsUI;
    [SerializeField] private EggCounterUI _eggCounterUI;
    [SerializeField] private WinLoseUI _winLoseUI;
    [Header("Settings")]

    [SerializeField] private float _delay;
    [SerializeField] private int _maxEggCount = 5;

    private GameState _currentGameState;
    private int _currentEggCount;
    void Awake()
    {
        Cursor.visible = true;
        Instance = this;
    }

    private void OnEnable()
    {
        ChangeGameState(GameState.Play);
    }

    public void OnEggCollected()
    {
        _currentEggCount += 1;

        _eggCounterUI.SetEggCounterText(_currentEggCount, _maxEggCount);

        if (_currentEggCount == _maxEggCount)
        {
            //WIN
            _eggCounterUI.SetEggCompleted();
            ChangeGameState(GameState.GameOver);
            _winLoseUI.OnGameWin();
        }
    }
    private IEnumerator OnGameOver()
    {
        yield return new WaitForSeconds(_delay);
        ChangeGameState(GameState.GameOver);
        _winLoseUI.OnGameLose();
    }

    public void PlayGameOver()
    {
        StartCoroutine(OnGameOver());
    }
    public void ChangeGameState(GameState gameState)
    {
        OnGameStateChanged?.Invoke(gameState);
        _currentGameState = gameState;
        Debug.Log("Game State: " + gameState);
    }

    public GameState GetCurrentGameState()
    {
        return _currentGameState;
    }
}
