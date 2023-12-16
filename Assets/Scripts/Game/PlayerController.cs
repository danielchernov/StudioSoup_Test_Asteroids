using System;
using UnityEngine;
using UnityEngine.InputSystem;
using AsteroidsTest.Core;

namespace AsteroidsTest.Game
{
    public class PlayerController : MonoBehaviour, IDamageable
    {
        [SerializeField]
        float _movementSpeed = 500;

        [SerializeField]
        float _rotationSpeed = 300;

        [SerializeField]
        int _maxHealth = 3;

        int _currentHealth = 0;

        Vector2 _movementDirection;

        [SerializeField]
        Rigidbody2D _playerRb;

        [SerializeField]
        InputActionReference _movementInput;

        [SerializeField]
        InputActionReference _fireInput;

        [SerializeField]
        Transform _firePoint;

        [SerializeField]
        AudioSource _playerAudio;

        [SerializeField]
        AudioClip[] _fireSFX;

        public static event Action OnTookDamage;
        public static event Action OnPlayerDeath;

        void Start()
        {
            _currentHealth = _maxHealth;
        }

        // Add/Remove Listener to Fire Action
        void OnEnable()
        {
            _fireInput.action.started += FireBullet;
        }

        void OnDisable()
        {
            _fireInput.action.started -= FireBullet;
        }

        void Update()
        {
            // Get Movement Input
            _movementDirection = _movementInput.action.ReadValue<Vector2>();
        }

        void FixedUpdate()
        {
            // Move and Rotate Player
            if (_movementDirection.y > 0)
            {
                _playerRb.AddForce(
                    transform.up * _movementDirection.y * _movementSpeed * Time.deltaTime
                );
            }

            if (_movementDirection.x != 0)
            {
                transform.Rotate(
                    Vector3.forward,
                    -_movementDirection.x * _rotationSpeed * Time.deltaTime
                );
            }
        }

        void FireBullet(InputAction.CallbackContext obj)
        {
            // Pool Bullet
            GameObject bullet = ObjectPooler.Instance.SpawnFromPool(
                "Bullets",
                _firePoint.position,
                _firePoint.rotation
            );

            //Play Fire SFX
            _playerAudio.PlayOneShot(_fireSFX[UnityEngine.Random.Range(0, _fireSFX.Length)], 0.5f);
        }

        public void TakeDamage(int Damage)
        {
            // Health - Damage
            OnTookDamage?.Invoke();

            if (_currentHealth <= 0)
            {
                DestroyObject();
            }
        }

        public void DestroyObject()
        {
            OnPlayerDeath?.Invoke();
            // Invoke dead event for GameManager
        }
    }
}
