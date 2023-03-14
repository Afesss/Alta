using UnityEngine;

namespace Settings
{
    [CreateAssetMenu(fileName = "PlayerSettings", menuName = "Settings/PlayerSettings", order = 1)]
    public class PlayerSettings : ScriptableObject
    {
        public float ProjectileForce = 15;
        public float PlayerForce = 5;
        public float AbsoluteTime = 10;
        public float MinCriticalSize = 0.5f;
        public LayerMask RayLayer;
    }
}
