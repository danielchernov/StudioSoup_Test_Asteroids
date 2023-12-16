using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using AsteroidsTest.Core;

namespace AsteroidsTest.Game
{
    public class PlayerController : MonoBehaviour, IDamageable
    {
        private static PlayerController _instance;
        public static PlayerController Instance
        {
            get
            {
                if (_instance == null)
                {
                    Debug.LogError("PlayerController is Null");
                }

                return _instance;
            }
        }

        [SerializeField]
        float _movementSpeed = 500;

        [SerializeField]
        float _rotationSpeed = 300;

        [SerializeField]
        int _maxHealth = 5;

        int _currentHealth = 0;

        bool _isInputLocked = false;

        Vector2 _movementDirection;

        [SerializeField]
        Rigidbody2D _playerRb;

        [SerializeField]
        InputActionReference _movementInput;

        [SerializeField]
        InputActionReference _fireInput;

        [SerializeField]
        Transform _firePoint;

        public static event Action<int> OnTookDamage;
        public static event Action OnPlayerDeath;

        private void Awake()
        {
            _instance = this;
        }

        void Start()
        {
            _currentHealth = _maxHealth;
        }

        // Add Listeners
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
            if (!_isInputLocked)
            {
                _movementDirection = _movementInput.action.ReadValue<Vector2>();
            }
        }

        void FixedUpdate()
        {
            // Move and Rotate Player
            if (_playerRb == null)
                return;

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

        public void TakeDamage(int Damage)
        {
            _currentHealth -= Mathf.Clamp(Damage, 0, _currentHealth);

            OnTookDamage?.Invoke(_currentHealth);

            if (_currentHealth <= 0)
            {
                DestroyObject();
            }
        }

        public void DestroyObject()
        {
            OnPlayerDeath?.Invoke();
            AudioManager.Instance.PlaySFX(AudioManager.AudioType.PlayerDeath);

            gameObject.SetActive(false);
        }

        public int GetMaxHealth()
        {
            return _maxHealth;
        }

        void FireBullet(InputAction.CallbackContext obj)
        {
            if (!_isInputLocked)
            {
                // Pool Bullet
                if (_firePoint == null)
                    return;

                GameObject bullet = ObjectPooler.Instance.SpawnFromPool(
                    "Bullets",
                    _firePoint.position,
                    _firePoint.rotation
                );

                //Play Fire SFX
                AudioManager.Instance.PlaySFX(AudioManager.AudioType.Fire);
            }
        }

        public void LockInput()
        {
            _isInputLocked = !_isInputLocked;
        }
    }
}
