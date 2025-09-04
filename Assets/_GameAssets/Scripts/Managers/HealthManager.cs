using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _currentHealth;

    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    public void Damage(int damageAmount)
    {
        if (_currentHealth > 0)
        {
            _currentHealth -= damageAmount;
            //TODO damage animation

            if (_currentHealth <= 0)
            {
                //TODO player is dead
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
