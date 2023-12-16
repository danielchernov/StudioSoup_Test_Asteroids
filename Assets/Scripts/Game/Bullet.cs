using UnityEngine;

namespace AsteroidsTest.Game
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField]
        float _bulletForce = 10;

        [SerializeField]
        Rigidbody2D _bulletRb;

        private void Start()
        {
            // Add Forward Force
            _bulletRb.velocity = Vector2.zero;
            Vector2 bulletDirection = transform.up;

            _bulletRb.AddForce(transform.up * _bulletForce, ForceMode2D.Impulse);
        }
    }
}
