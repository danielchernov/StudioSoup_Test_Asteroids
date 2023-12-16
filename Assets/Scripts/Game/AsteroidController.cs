using System;
using UnityEngine;
using AsteroidsTest.Core;

namespace AsteroidsTest.Game
{
    public class AsteroidController : MonoBehaviour, IDamageable
    {
        [SerializeField]
        int _maxHealth = 1;

        int _currentHealth = 0;

        public static event Action OnEnemyDeath;

        void Start()
        {
            _currentHealth = _maxHealth;
        }

        public void TakeDamage(int Damage)
        {
            // Health-Damage;
        }

        public void DestroyObject()
        {
            OnEnemyDeath?.Invoke();

            // SetActive false?
        }
    }
}
