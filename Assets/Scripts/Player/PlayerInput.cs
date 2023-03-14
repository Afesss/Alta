using System;
using UnityEngine;

namespace Player
{
    public class PlayerInput : MonoBehaviour
    {
        public event Action TouchStart;
        public event Action<float> Touched;
        public event Action TouchEnd;
        
        private float _tapTime;
        
        private void OnMouseOver()
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                if(_tapTime == 0)
                    TouchStart?.Invoke();
                
                _tapTime += Time.deltaTime;
                Touched?.Invoke(_tapTime);
            }
        }

        private void OnMouseUp()
        {
            TouchEnd?.Invoke();
            _tapTime = 0;
        }
    }
}
