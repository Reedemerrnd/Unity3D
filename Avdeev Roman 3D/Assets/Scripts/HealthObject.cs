using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class HealthObject : MonoBehaviour
{
    public UnityEvent OnDeath;
    [SerializeField]
    private float _currentHealth;
    [SerializeField]
    private float _maxHealth;
    public float MaxHealth => _maxHealth;
    public float CurrentHealth 
    {
        get => _currentHealth;
        protected set
        {
            _currentHealth = value;
            if (_currentHealth > _maxHealth) _currentHealth = _maxHealth;
            else if (_currentHealth <= 0)
            {
                _currentHealth = 0;
                OnDeath?.Invoke();
            }
        }
    }
    protected virtual void Start()
    {
        _currentHealth = _maxHealth;
    }

}
