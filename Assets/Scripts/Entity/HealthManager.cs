using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour, IHittable
{
    public Image healthBar;

    [SerializeField]
    private float _maxHealth;

    [SerializeField]
    private float _armor = 0f;

    private float _health = 5f;

    public delegate void HealthEvent();
    public HealthEvent OnDie;
    public HealthEvent OnTakeDamage;
    public HealthEvent OnRestore;
    public HealthEvent OnRevive;

    private bool _isAlive = true;

    private void Start()
    {
        _health = _maxHealth;
        if (healthBar != null) healthBar.fillAmount = _health / _maxHealth;
    }


    public void TakeDamage(float damageTaken)
    {
        if (damageTaken > _armor)        // If damage is higher than the damage negation the entity has
        {
            if (OnTakeDamage != null) OnTakeDamage.Invoke();
            _health -= damageTaken;
        }

        if (_health <= 0)
        {
            if (OnDie != null) OnDie.Invoke();
            _isAlive = false;
        }

        if (healthBar != null) healthBar.fillAmount = _health / _maxHealth;     // Displays health as a bar
    }

    public void GainHealth(float healthGained)
    {
        if (_health > 0)
        {
            if (healthGained + _health > _maxHealth)
            {
                _health = _maxHealth;
                if (OnRestore != null) OnRestore.Invoke();
            }

            else if (_health + healthGained <= +_maxHealth)
            {
                _health += healthGained;
                if (OnRestore != null) OnRestore.Invoke();
            }

        }

        else if (_health <= 0)
        {
            if (OnRevive != null) OnRevive.Invoke();
            _health += healthGained;
            _isAlive = true;
        }

        else
        {
            return;
        }

        if (healthBar != null) healthBar.fillAmount = _health / _maxHealth;

    }

    public float GetHealth()
    {
        return _health;
    }

    public bool GetIsAlive()
    {
        return _isAlive;
    }
}
