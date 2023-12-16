using System;
using System.Collections;
using UnityEngine;
using AsteroidsTest.Core;
using Random = UnityEngine.Random;

namespace AsteroidsTest.Game
{
    public class AsteroidController : MonoBehaviour, IDamageable, IPerishable
    {
        [SerializeField]
        float _asteroidSize = 1;

        [SerializeField]
        int _asteroidScore = 10;

        [SerializeField]
        float _asteroidSpeed = 5;

        [SerializeField]
        int _maxHealth = 1;

        int _currentHealth = 0;

        [SerializeField]
        Rigidbody2D _asteroidRb;

        public static event Action<int> OnEnemyDeath;

        void OnEnable()
        {
            // Set Health and Size
            _currentHealth = _maxHealth;
            transform.localScale = Vector3.one * _asteroidSize;

            // Push Asteroid
            Vector2 lookToCenter = (Vector3.zero - transform.position).normalized;

            Vector2 asteroidDirection = new Vector2(
                lookToCenter.x + Random.Range(-1f, 1f),
                lookToCenter.y + Random.Range(-1f, 1f)
            );

            _asteroidRb.AddForce(
                asteroidDirection.normalized * _asteroidSpeed,
                ForceMode2D.Impulse
            );

            _asteroidRb.AddTorque(Random.Range(0, 100));

            // Set Deactivate After Time
            StartCoroutine(DeactivateAfterTime(30));
        }

        public void TakeDamage(int Damage)
        {
            // Health-Damage;
        }

        public void DestroyObject()
        {
            OnEnemyDeath?.Invoke(_asteroidScore);

            // SetActive false?
        }

        public IEnumerator DeactivateAfterTime(float timeToDeactivate)
        {
            yield return new WaitForSeconds(timeToDeactivate);
            gameObject.SetActive(false);
        }
    }
}
