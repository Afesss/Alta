using System.Collections;
using UnityEngine;

namespace Obstacles
{
    public class Obstacle : MonoBehaviour
    {
        [SerializeField] private MeshRenderer meshRenderer;
        [SerializeField] private Collider thisCollider;
        [SerializeField] private ParticleSystem particle;
        [SerializeField] private Material yellowMaterial;
        
        private void OnTriggerEnter(Collider other)
        {
            meshRenderer.material = yellowMaterial;
            StartCoroutine(WaitToDestroy());
        }

        private IEnumerator WaitToDestroy()
        {
            thisCollider.enabled = false;
            yield return new WaitForSeconds(0.5f);
            meshRenderer.enabled = false;
            particle.Play();

            while (particle.isPlaying)
            {
                yield return null;
            }
            
            Destroy(gameObject);
        }
    }
}
