using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace coucou
{

public class health : MonoBehaviour
{
    // Field
    [SerializeField, ValidateInput("ValidateMaxHealth")] int _maxHealth;
    [SerializeField] int _curentHealth;

    bool _isDie;

    [SerializeField] UnityEvent _onDamage;

    // Properties



    // Methodes
    #region EditorParametre

    private void Start()
    {
        _curentHealth = _maxHealth;
    }

    private void Reset()
    {
        Debug.Log("Reset");
        _maxHealth = 100;
    }

    bool ValidateMaxHealth()
    {
        // Guards
        if (_maxHealth <= 0)
        {
            _maxHealth = 100;
            Debug.LogWarning("Pas de HPMax négatif");
            return false;
        }
        return true;
    }

    #endregion



    void Regen(int amount)
    {
        // Guards
        if (amount < 0) 
        {
            throw new ArgumentException("Mauvaise valeur, valeur négative");
        }

        if(_isDie)
        {
            return;
        }

        _curentHealth += amount;

        _curentHealth = Math.Clamp(_curentHealth + amount, 0, _maxHealth);

        Debug.Log("Heal");
    }

    void TakeDamage(int amount)
    {
        // Guards
        if (amount < 0) 
        {
            throw new ArgumentException("Mauvaise valeur, valeur négative");
        }

        _curentHealth -= amount;

        _curentHealth = Math.Clamp(_curentHealth - amount, 0, _maxHealth);

        if (_curentHealth <= 0) Die();

        _onDamage.Invoke();

        Debug.Log("Damage");
    }

    void Die()
    {
        _isDie = true;
        _curentHealth = 0;

        Debug.Log("Die");
    }

    void resurection()
    {
         if (_isDie)
         {
             _isDie = false;
             _curentHealth = 1;

             Debug.Log("Resurection");
         }
    }

    [Button] void coucou() => TakeDamage(10);
    [Button] void coucou2() => Regen(5);
    [Button] void coucou3() => resurection();
}

}
