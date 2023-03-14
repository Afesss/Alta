using DG.Tweening;
using UnityEngine;

namespace Obstacles
{
    public class Tower : MonoBehaviour
    {
        [SerializeField] private Transform door;

        public void OpenDoor()
        {
            door.DORotate(new Vector3(0, 90, 0), 0.5f);
        }
    }
}
