using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace Player
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private Rigidbody rb;

        public event Action Destroyed;

        private const string PlayerTag = "Player";
        public float StartScale { get; private set; }
        
        private Tween _moveTween;

        private void Awake()
        {
            StartScale = transform.localScale.x;
        }
        
        public void MoveTo(Vector3 position)
        {
            _moveTween = transform.DOMove(position, 0.5f);
        }

        public void AddForce(Vector3 direction, float force)
        {
            _moveTween?.Kill();
            rb.AddForce(direction * force, ForceMode.Impulse);
        }

        public void SetScale(Vector3 scale)
        {
            transform.localScale = scale;
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag(PlayerTag))
                return;
            StartCoroutine(WaitToDestroy());
        }

        private IEnumerator WaitToDestroy()
        {
            yield return new WaitForSeconds(0.05f);
            Destroy(gameObject);
            Destroyed?.Invoke();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if(collision.collider.CompareTag(PlayerTag))
                return;
            
            Destroy(gameObject);
            Destroyed?.Invoke();
        }
    }
}
