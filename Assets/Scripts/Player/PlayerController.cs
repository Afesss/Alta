using System;
using System.Collections;
using Architecture.Services;
using Obstacles;
using Settings;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerSettings settings;
        [SerializeField] private PlayerInput playerInput;
        [SerializeField] private Rigidbody rb;

        public event Action PlayerLose;
        public event Action PlayerWin;

        private const string TowerTag = "Tower";
        private const float ProjectileOffset = 1.3f;
        
        private bool _isActive;
        private bool _isShoot;
        
        private float _scale;
        
        private Projectile _projectile;
        private RaycastHit _raycastHit;

        private Vector3 _directionToTower;
        private Tower _tower;
        private GameFactory _gameFactory;

        private void Start()
        {
            playerInput.TouchStart += PlayerInputOnTouchStart;
            playerInput.TouchEnd += PlayerInputOnTouchEnd;
            playerInput.Touched += PlayerInputOnTouched;
        }

        private void OnDestroy()
        {
            playerInput.TouchStart -= PlayerInputOnTouchStart;
            playerInput.TouchEnd -= PlayerInputOnTouchEnd;
            playerInput.Touched -= PlayerInputOnTouched;
        }

        public void Initialize(Tower tower, GameFactory gameFactory)
        {
            _tower = tower;
            _gameFactory = gameFactory;
            _directionToTower = (_tower.transform.position - transform.position).normalized;
            _isActive = true;
            rb.isKinematic = true;
        }

        private void PlayerInputOnTouchStart()
        {
            CreateProjectile();
            _scale = transform.localScale.x;
        }

        private void PlayerInputOnTouched(float time)
        {
            TryRescaleObjects(time);
        }

        private void PlayerInputOnTouchEnd()
        {
            PushProjectile();
        }

        private void CreateProjectile()
        {
            if(_isActive == false || _projectile != null)
                return;

            _projectile = _gameFactory.CreateProjectile(transform.position);
            _projectile.MoveTo(transform.position + _directionToTower * ProjectileOffset);
            _projectile.Destroyed += ProjectileOnDestroyed;
        }

        private void ProjectileOnDestroyed()
        {
            _isShoot = false;
            var hit = Physics.SphereCast(transform.position, transform.localScale.x / 2, _directionToTower,
                out _raycastHit, 50, settings.RayLayer);
            
            if (hit && _raycastHit.collider.CompareTag(TowerTag))
            {
                _isActive = false;
                rb.isKinematic = false;
                rb.AddForce((_directionToTower + Vector3.up).normalized * settings.PlayerForce, ForceMode.Impulse);
                StartCoroutine(TowerOpenDoor());
            }
        }

        private IEnumerator TowerOpenDoor()
        {
            while ((_tower.transform.position - transform.position).magnitude > 5)
            {
                yield return null;
            }
            _tower.OpenDoor();
        }

        private void PushProjectile()
        {
            if (_projectile != null && !_isShoot)
            {
                _isShoot = true;
                _projectile.AddForce(_directionToTower, settings.ProjectileForce);
            } 
        }

        private void TryRescaleObjects(float time)
        {
            if(!_isActive || _isShoot || _projectile == null)
                return;
            
            if (transform.localScale.x >= settings.MinCriticalSize)
            {
                float percent = time / settings.AbsoluteTime;
                var scale = _scale - _scale * percent;
                transform.localScale = new Vector3(scale, scale, scale);
                var projectileScale = _projectile.StartScale + _scale * percent;
                _projectile.SetScale(new Vector3(projectileScale, projectileScale, projectileScale));
            }
            else
            {
                SetLose();
            }
        }

        private void SetLose()
        {
            if(_projectile != null)
                Destroy(_projectile);
            _isActive = false;
            PlayerLose?.Invoke();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.CompareTag(TowerTag))
            {
                PlayerWin?.Invoke();
                Destroy(gameObject);
            }  
        }
    }
}
