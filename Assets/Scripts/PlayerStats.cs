using UnityEngine;
using UnityEngine.Events;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _moveSpeed;
    [SerializeField] [Range(0, 1f)] private float _basicMoveSpeedModifier;
    
    private float _currentMoveSpeedModifier;
    private float _currentHealth;
    private float _pickAxeDamage;
    private readonly UnityEvent _playerDead = new UnityEvent();

    public float MoveSpeed => _moveSpeed;
    public float MoveSpeedModifier => _currentMoveSpeedModifier;
    public float CurrentHealth => _currentHealth;
    public float MaxHealth => _maxHealth;
    public float PickAxeDamage => _pickAxeDamage;

    private void Start()
    {
        //Temp
        _pickAxeDamage = 1;
        _currentHealth = _maxHealth;
        _currentMoveSpeedModifier = _basicMoveSpeedModifier;
    }

    private void Update()
    {

    }

    private void ApplyDamage(float damage)
    {
        _currentHealth -= damage;
        if(_currentHealth <= 0)
        {
            _playerDead.Invoke();
        }
        FindObjectOfType<UIStatsController>().UpdateHPBar();
    }

    private void ModifyMoveSpeed(float modifier)
    {
        _currentMoveSpeedModifier = _currentMoveSpeedModifier * modifier;
    }
}
