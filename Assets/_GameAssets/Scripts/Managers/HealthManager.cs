using UnityEngine;

public class HealthManager : MonoBehaviour
{   
    public static HealthManager Instance { get; private set;}
    [Header("References")]
    [SerializeField] private PlayerHealthUI _playerHealthUI;
    [Header("Settings")]
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _currentHealth;

    void Awake() 
    {
        Instance = this;
    }
    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    public void Damage(int damageAmount)
    {
        if (_currentHealth > 0)
        {
            _currentHealth -= damageAmount;
            _playerHealthUI.AnimateDamage();


            if (_currentHealth <= 0)
            {
                GameManager.Instance.PlayGameOver();
            }
        }
    }

    public void Heal(int healAmount)
    {
        if (_currentHealth + healAmount >= _maxHealth)
        {
            _currentHealth = _maxHealth;
        }
        else if (_currentHealth + healAmount < _maxHealth)
        {
            _currentHealth += healAmount;
        }
    }

    public int CurrentHealth => _currentHealth;
}
