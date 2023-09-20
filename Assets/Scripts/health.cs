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
        public static health Player
        {
            get;
            private set;
        }

        void getPlayer()
        {
            var player = GameObject.Find("Player");
            player = GameObject.FindGameObjectWithTag("Player");
        }

        // Field
        [SerializeField, ValidateInput("ValidateMaxHealth")] int _maxHealth;
        [SerializeField] int _currentHealth;

        bool _isDie;

        [SerializeField] UnityEvent _onDamage;

        // Properties
        public int CurrentHealth {  get => _currentHealth; set => _currentHealth = value; }


        // Methodes
        #region EditorParametre

        private void Start()
        {
            CurrentHealth = _maxHealth;
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

            _currentHealth += amount;

            _currentHealth = Math.Clamp(_currentHealth + amount, 0, _maxHealth);

            Debug.Log("Heal");
        }

        void TakeDamage(int amount)
        {
            // Guards
            if (amount < 0) 
            {
                throw new ArgumentException("Mauvaise valeur, valeur négative");
            }

            _currentHealth -= amount;

            _currentHealth = Math.Clamp(_currentHealth - amount, 0, _maxHealth);

            if (_currentHealth <= 0) Die();

            _onDamage.Invoke();

            Debug.Log("Damage");
        }

        void Die()
        {
            _isDie = true;
            _currentHealth = 0;

            Debug.Log("Die");
        }

        void resurection()
        {
             if (_isDie)
             {
                 _isDie = false;
                 _currentHealth = 1;

                 Debug.Log("Resurection");
             }
        }

        [Button] void coucou() => TakeDamage(10);
        [Button] void coucou2() => Regen(5);
        [Button] void coucou3() => resurection();
    }

}
