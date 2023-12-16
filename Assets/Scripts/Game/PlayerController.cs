using UnityEngine;
using UnityEngine.InputSystem;

namespace AsteroidsTest.Game
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        float _movementSpeed = 500;

        [SerializeField]
        float _rotationSpeed = 300;

        [SerializeField]
        float _bulletForce = 10;

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

            // Fire Bullets
            // if (_fireInput.action)
            // {
            //     FireBullet();
            // }
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
            GameObject bullet = ObjectPooler.Instance.SpawnFromPool(
                "Bullets",
                _firePoint.position,
                _firePoint.rotation
            );

            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();

            bulletRb.velocity = Vector2.zero;
            Vector2 bulletDirection = _firePoint.up;

            bulletRb.AddForce(_firePoint.up * _bulletForce, ForceMode2D.Impulse);

            _playerAudio.PlayOneShot(_fireSFX[Random.Range(0, _fireSFX.Length)], 0.5f);
        }
    }
}
